using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;


public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    private float timeBtwDamage = 1.5f;//�����˺�֮ǰ�ļ��ʱ��
    public Slider HPbar;
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public Transform firePoint; // �����
    public float fireRate = 1f; // ������
    public int health = 100; // BOSS����ֵ
    private float timer = 0f;
    private int phase = 1; // BOSS�׶�
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }
        HPbar.value = health;
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            AttackPattern();
            timer = 0f;
        }
    }

    void AttackPattern()
    {
        switch (phase)
        {
            case 1:
                FireCircularBullets(12); // 12�����ε�Ļ
                break;
            case 2:
                FireStraightAtPlayer(); // ֱ��׷�����
                break;
            case 3:
                FireRandomBullets(8); // ��������ӵ�
                break;
        }
    }
    void FireCircularBullets(int bulletCount)
    {
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            FireBullet(direction);
        }
    }
    void FireStraightAtPlayer()
    {
        if (player)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            FireBullet(direction);
        }
    }
    void FireRandomBullets(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0).normalized;
            FireBullet(randomDirection);
        }
    }
    void FireBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity = direction * 5f; // �ӵ��� X/Y ���ƶ�
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 70) phase = 2; // ����ֵ���٣�������һ�׶�
        if (health <= 30) phase = 3;
        if (health <= 0) Destroy(gameObject); // BOSS����
    }
}
   
