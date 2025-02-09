using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriangle : Enemy
{
    [Header("Settings")]
   
    [SerializeField] private GameObject[] splitPrefabs; // ���Ѻ��BossԤ��������
    [SerializeField] private float splitRadius = 2f; // �������ɰ뾶
    [SerializeField] private int currentSplitLevel = 0; // ��ǰ���Ѳ㼶��0=��ʼ��
    public float[] splitThresholds = { 0.7f, 0.6f };//������ֵΪ�ٷ�֮����ʱ����

    private bool isSplitTriggered = false;

    protected override void Start()
    {
      
        base.Start(); // ���û����ʼ��Ѫ��
       

    }

    override public void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        CheckSplitCondition();
       
        
    }

    private void FixedUpdate()
    {
        playerTarget = GameObject.FindWithTag("Player").transform;
        base.HandleMovement();
        base.HandleShooting();
    }



    // ����Ƿ�ﵽ��������
    private void CheckSplitCondition()
    {
        float healthPercentage = (float)currentHealth / maxHealth;

        // ���ݲ㼶�жϷ�����ֵ��ʾ�������͡�70%�����͡�60%��
        

        if (currentSplitLevel < splitThresholds.Length &&
            healthPercentage <= splitThresholds[currentSplitLevel] &&
            !isSplitTriggered)
        {
            isSplitTriggered = true;
            StartCoroutine(SplitBoss());
           
        }
    }

    // �����߼�
    private IEnumerator SplitBoss()
    {
        // ���ŷ��Ѷ����������š�����Ч����
        
        yield return new WaitForSeconds(0.1f); 

        // ������Boss
        foreach (var prefab in splitPrefabs)
        {
            Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * splitRadius;
            GameObject childBoss = Instantiate(prefab, spawnPos, Quaternion.identity);

            // ���ݷ��Ѳ㼶��Ϣ
            if (childBoss.TryGetComponent<BossTriangle>(out var childController))
            {
                childController.currentSplitLevel = currentSplitLevel + 1;
            }
        }

      
    }

   
}
