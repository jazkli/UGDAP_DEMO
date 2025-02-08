using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour {
    
    [SerializeField] private GameObject roomlight;
    [SerializeField] private Camera maincamera;
    [SerializeField] private List<Room> rooms;
    [SerializeField] private GameObject playerPrefab;

    private static GameInstance Singleton;
    private UPlayerController player;
    
    private void Start() {

        if (Singleton == null) {
            Singleton = this;//实现单例
        }
        
        player = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity).GetComponent<UPlayerController>();
        player.SetUpRooms(rooms);//传递房间数据
        player.SetUpCamera(maincamera);//传递摄像机数据

    }

    public GameObject GetLight() {
        return roomlight;
    }
    
    public static GameInstance GetSingleton() {
        return Singleton;
    }
    
}
