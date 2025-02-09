using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("����ֵ����")]
    public int maxHealth = 1000;
    public int currentHealth;
    [Header("�ƶ�����")]
    public float moveSpeed = 3f;
    public float minDistance = 0f;//������
    [Header("�������")]
    public GameObject projectilePrefab;
    public float fireRate = 1f;
    public Transform[] firePoints;

    public Transform playerTarget;
    public float nextFireTime;
    protected virtual void Start()
    {
        currentHealth = maxHealth;

    }
    virtual public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // ������������/��Ч
        Destroy(gameObject);
    }
    virtual public void HandleMovement()
    {
        if (playerTarget == null)

        {
            Debug.LogError("δ�ҵ���Ҷ�����ȷ�������'Player'��ǩ");
            
        }
            {
            // ���㳯����ҵķ���
            Vector2 direction = (playerTarget.position - transform.position).normalized;

            //Debug.DrawLine(Vector2.zero,direction,Color.white,2.5f);

            // �ƶ�
            transform.Translate(
                moveSpeed * Time.deltaTime * direction
            );
        }
    }
    virtual public void HandleShooting()
    {
        if (Time.time >= nextFireTime)
        {
            foreach (Transform point in firePoints)
            {
                Instantiate(
                    projectilePrefab,
                    point.position,
                    point.rotation
                );
            }
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}
