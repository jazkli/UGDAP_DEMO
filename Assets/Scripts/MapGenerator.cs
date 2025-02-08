using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    
    [SerializeField] private GameObject RoomWith4Doors;
    [SerializeField] private GameObject RoomWithNoDoor;
    [SerializeField] private List<GameObject> RoomWith3Doors;
    [SerializeField] private List<GameObject> RoomWith2Doors;
    [SerializeField] private List<GameObject> RoomWith1Doors;
    [SerializeField] private int bossCount = 3;
    
    private int[,] mapMatrix = new int[10, 10];
    private int startRoomIndex, endRoomIndex;
    private int[] bossRooms = new int[10];
    private List<int> path;
    private int[] preNodeMatrix = new int[100];
    private int[,] connectionMatrix = new int[10, 10];
    
    // Start is called before the first frame update
    void Start() {

        for (int i = 0; i < 100; ++i) {
            preNodeMatrix[i] = -1;
        }

        GenerateMap();
        GenerateRoomTypeInPath();

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void GenerateMap() {
        // 生成起始点
        startRoomIndex = Random.Range(0, 100);
        do {
            endRoomIndex = Random.Range(0, 100);
        } while (GetManhattanDistanceBetweenPoints(startRoomIndex, endRoomIndex) < 12);
        // 获取起始点路径
        path = BFSGetPath();
        GenerateBranch();
    }

    private List<int> BFSGetPath() {
        Queue<int> q = new Queue<int>();
        int[] dx = new[] { 0, 1, 0, -1 }, dy = new[] { 1, 0, -1, 0 };
        HashSet<int> visited = new HashSet<int>();
        
        q.Enqueue(startRoomIndex);
        visited.Add(startRoomIndex);
        
        while (q.Count > 0) {
            int tempIndex = q.Dequeue();
            int tx = 0, ty = 0;
            GetXYByRoomIndex(tempIndex, ref tx, ref ty);
            for (int i = 0; i < 4; ++i) {
                int x = dx[i] + tx, y=  dy[i] + ty;
                if ((x >= 0 && x < 100) && (y >= 0 && y < 100)) {
                    int index = GetRoomIndexByXY(x, y);
                    if (index == endRoomIndex) {
                        break;
                    }
                    if (!visited.Contains(index)) {
                        preNodeMatrix[index] = tempIndex;
                        visited.Add(index);
                        q.Enqueue(index);
                    }
                }      
            }
        }
        
        List<int> path = new List<int>();
        RecursiveCalculatePath(endRoomIndex, -1, ref path);
        return path;
    }
    
    private void GenerateBranch() {
        var visitedRoom = new HashSet<int>();
        foreach (var node in path) {
            visitedRoom.Add(node);
        }
        for (int i = 0; i < path.Count; ++i) {
            float branchPossibility = Random.Range(0f, 1f);
            if (branchPossibility > 0.2) {
                int branchDeep = Random.Range(1, 6);
                RecursiveExtendBranch(path[i], -1, ref visitedRoom, branchDeep);
            }
        }
    }

    private void GenerateRoomTypeInPath() {
        for (int i = 0; i < path.Count; ++i) {
            
        }
    }
    
    // 工具方法
    int GetManhattanDistanceBetweenPoints(int startIndex, int endIndex) {
        int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
        GetXYByRoomIndex(startRoomIndex, ref x1, ref y1);
        GetXYByRoomIndex(endRoomIndex, ref x2, ref y2);
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }
    
    private int GetRoomIndexByXY(int x, int y) {
        return x * 10 + y;
    }

    private void GetXYByRoomIndex(int index, ref int x, ref int y) {
        x = index / 10;
        y = index % 10;
    }
    
    // 1：B在A上， 2：B在A右，4：B在A下，8：B在A左
    private int GetRoomRelativePosition(int ARoom, int BRoom) {
        int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
        GetXYByRoomIndex(ARoom, ref x1, ref y1);
        GetXYByRoomIndex(BRoom, ref x2, ref y2);
        if (x2 < x1) {
            return 1;
        } else if (x2 > x1) {
            return 2;
        } else if (y2 < y1) {
            return 4;
        } else {
            return 8;
        }
    } 
    
    private void RecursiveCalculatePath(int currentRoom, int lastRoom, ref List<int> path) {
        if (currentRoom == -1) {
            return;
        }

        if (lastRoom != -1) {
            int ABRelativePosition = GetRoomRelativePosition(currentRoom, lastRoom);
            int BARelativePosition = GetRoomRelativePosition(lastRoom, currentRoom);
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            GetXYByRoomIndex(currentRoom, ref x1, ref y1);
            GetXYByRoomIndex(lastRoom, ref x2, ref y2);
            connectionMatrix[x1, y1] |= ABRelativePosition;
            connectionMatrix[x2, y2] |= BARelativePosition;
        }
        RecursiveCalculatePath(preNodeMatrix[currentRoom], currentRoom, ref path);
        path.Add(currentRoom);
    }

    
    private void RecursiveExtendBranch(int currentRoom, int lastRoom, ref HashSet<int> visitedRoom, int deep) {

        if (deep == 0) {
            return;
        }
        
        visitedRoom.Add(currentRoom);
        int currentRoomX = 0, currentRoomY = 0, lastRoomX = 0, lastRoomY = 0;
        GetXYByRoomIndex(currentRoom, ref currentRoomX, ref currentRoomY);
        GetXYByRoomIndex(lastRoom, ref lastRoomX, ref lastRoomY);
        
        if (lastRoom != -1) {
            int ABRelativePosition = GetRoomRelativePosition(currentRoom, lastRoom);
            int BARelativePosition = GetRoomRelativePosition(lastRoom, currentRoom);
            connectionMatrix[currentRoomX, currentRoomY] |= ABRelativePosition;
            connectionMatrix[lastRoomX, lastRoomY] |= BARelativePosition;
        }
        
        for (int tryCount = 4; tryCount > 0; tryCount--) {
            int[] dx = new[] { 0, 1, 0, -1 }, dy = new[] { 1, 0, -1, 0 };
            int extendDirection = Random.Range(1, 5);
            int x = dx[extendDirection] + currentRoomX, y = dy[extendDirection] + currentRoomY;
            int newRoomIndex = GetRoomIndexByXY(x, y);
            if (!(x < 0 || x >= 10 || y < 0 || y >= 10) && !visitedRoom.Contains(newRoomIndex)) {
                RecursiveExtendBranch(newRoomIndex, currentRoom, ref visitedRoom, deep - 1);
                break;
            }
        }
    }
    

    
}
