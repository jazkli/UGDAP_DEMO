using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour {

    [SerializeField] private GameObject light;
    [SerializeField] private Camera camera;
    [SerializeField] private List<Room> rooms;
    [SerializeField] private GameObject playerPrefab;

    private static GameInstance Singleton;
    private UPlayerController player;
    
    private void Start() {

        if (Singleton == null) {
            Singleton = this;
        }
        
        player = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity).GetComponent<UPlayerController>();
        player.SetUpRooms(rooms);
        player.SetUpCamera(camera);

    }

    public GameObject GetLight() {
        return light;
    }
    
    public static GameInstance GetSingleton() {
        return Singleton;
    }
    public GameObject GetPlayer()
    {
        return playerPrefab;
    }

}
