using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public int skillID;//����ID
    public string skillName;//������
    public Sprite skillSprite;//����ͼ��
    [TextArea] public string description;//��������

    public bool isUnlocked;//�ж��Ƿ��������
}
