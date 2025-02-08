using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public SkillData activeSkill;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    [Header("UI")]
    public Image skillImage;
    public Text skillNameText,skillDesText;

    public void DisplaySkillInfo()
    {
        skillImage.sprite = activeSkill.skillSprite;
        skillNameText.text = activeSkill.skillName;
        skillDesText.text ="√Ë ˆ£∫\n"+ activeSkill.description;
    }
}
