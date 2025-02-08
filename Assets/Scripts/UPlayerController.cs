using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UPlayerController : MonoBehaviour {
    
    [SerializeField] private GameObject attackComponentGameObject;
    [SerializeField] private float fireDeltaTime = 1.0f;
    
    private float fireTimer = 0f;

    private List<Room> rooms;
    private Room currentRoom;
    private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    [SerializeField]
    private float moveSpeed = 5f;
    
    // Update is called once per frame
    void Update()
    {
        // 获取输入
        float moveX = Input.GetAxis("Horizontal"); // 左右移动
        float moveY = Input.GetAxis("Vertical");   // 上下移动

        // 计算移动向量
        Vector2 movement = new Vector2(moveX, moveY);

        // 移动对象
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Mouse0)) {
            fireTimer += 10 * Time.deltaTime;
            if (fireTimer >= fireDeltaTime) {
                fireTimer = 0f;
                Vector3 pressPosition = camera.ScreenToWorldPoint(Input.mousePosition);
                pressPosition.z = 0f;
                Vector3 fireDirection = (pressPosition - transform.position).normalized;
                attackComponentGameObject.GetComponent<AbstractAttackComponent>().Attack(transform.position, fireDirection);
            }
        }
    }

    public void SetUpRooms(List<Room> rooms) {
        this.rooms = rooms;
        currentRoom = rooms[0];
        foreach (Room room in rooms) {
            room.OnPlayerExitRoom += OnPlayerExitRoom; 
            room.OnPlayerEnterRoom += OnPlayerEnterRoom;
        }
    }

    public void SetUpCamera(Camera camera) {
        this.camera = camera;
    }

    private void OnPlayerEnterRoom(object sender, EventArgs e) {
        Room newRoom = sender as Room;
        if (newRoom != currentRoom) {
            Vector2 currentRoomPosition = currentRoom.transform.position;
            Vector2 newRoomPosition = newRoom.transform.position;
            currentRoom = newRoom;
            if (newRoomPosition.x > currentRoomPosition.x) {
                camera.transform.position += new Vector3(17, 0, 0);
            } else if (newRoomPosition.x < currentRoomPosition.x) {
                camera.transform.position += new Vector3(-17, 0, 0);
            } else if (newRoomPosition.y > currentRoomPosition.y) {
                camera.transform.position += new Vector3(0, 10, 0);
            } else {
                camera.transform.position += new Vector3(0, -10, 0);
            }
            GameInstance.GetSingleton().GetLight().transform.position = currentRoom.transform.position;
        }
    }

    private void OnPlayerExitRoom(object sender, EventArgs e) {
        Debug.Log("Leave Room");
    }

    
}
