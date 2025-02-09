using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] protected float flySpeed2 = 20f;
    [SerializeField] protected Rigidbody2D rb2;
    [SerializeField] protected int damage2 = 20;
    private Vector2 direction;
    void Start()
        {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;

            // �����ӵ����ƶ�����
            rb2.velocity = flySpeed2 * direction;
            Destroy(gameObject, 3f);
        }
    }

 
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<UPlayerController>(out var Player))
        {
            // �����˺�ֵ
            
            Destroy(gameObject);
        }
    }





    /* protected void OnCollisionEnter2D(Collision2D other)
     {
     if (other.gameObject.TryGetComponent<UPlayerController>(out var player))
     {
         // �����˺�ֵ
         player.TakeDamage(damage);
         Destroy(gameObject);
     }*/


}
