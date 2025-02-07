using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // 确保玩家有 "Player" Tag
        {
            Debug.Log("子弹击中了玩家！");
            Destroy(gameObject); // 子弹销毁
            //other.GetComponent<PlayerHealth>()?.TakeDamage(10); // 让玩家受伤
        }
    }

}
