using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public int skillID;//技能ID
    public string skillName;//技能名
    public Sprite skillSprite;//技能图标
    [TextArea] public string description;//技能描述

    public bool isUnlocked;//判断是否解锁技能
}
