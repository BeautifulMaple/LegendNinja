using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private SkillList skillList;
    private WeaponHandler weaponHandler;

    private void Start()
    {
        skillList = LoadSkills();
        weaponHandler = FindObjectOfType<WeaponHandler>();
    }

    private SkillList LoadSkills()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("skills");
        return JsonUtility.FromJson<SkillList>(jsonFile.text);
    }

    public SkillList GetSkillList()
    {
        // ���� skillList�� ���� �ε���� �ʾҴٸ�, LoadSkills() ȣ��
        if (skillList == null)
        {
            skillList = LoadSkills();
        }
        return skillList;
    }
    public void ApplySkill(int skillId)
    {
        SkillData skill = skillList.skills.FirstOrDefault(s => s.id == skillId);
        if (skill == null) return;

        switch (skill.type)
        {
            case "power":
                weaponHandler.Power += skill.value;
                Debug.Log($"power{weaponHandler.Power}");
                break;
            case "delay":
                weaponHandler.Delay+= skill.value;
                break;
            case "speed":
                weaponHandler.AttackSpeed += skill.value;
                break;

        }

        Debug.Log($"{skill.name} �����! ���� Power: {weaponHandler.Power}, Speed: {weaponHandler.AttackSpeed}");
    }


}
