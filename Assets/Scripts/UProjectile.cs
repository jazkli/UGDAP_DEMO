using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UProjectile : MonoBehaviour {
    [SerializeField] private float flySpeed = 20f;
    [SerializeField] private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update() {
        rigidbody2D.velocity = flySpeed * transform.up;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
    }
}
