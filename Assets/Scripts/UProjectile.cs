using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UProjectile : MonoBehaviour
{
    [SerializeField] protected float flySpeed = 20f;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected int damage=20;
    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    private void Update() {
       rb.velocity = flySpeed * transform.up;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out var currentEnemy))
        {
            // ´«µÝÉËº¦Öµ
            currentEnemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    /*private void OnDrawGizmos()

     {
         Gizmos.color = Color.red;
         Gizmos.DrawRay(transform.position, transform.up * 2f);
     }*/


}
