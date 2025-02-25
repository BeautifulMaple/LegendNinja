using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillData;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    private List<Skill> skills;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        LoadSkills();
    }

    private void LoadSkills()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("skills");
        if (jsonFile != null)
        {
            SkillList skillList = JsonUtility.FromJson<SkillList>(jsonFile.text);
            skills = skillList.skills;
            Debug.Log("��ų ������ �ε� �Ϸ�!");
        }
        else
        {
            Debug.LogError("��ų Json ������ ã�� �� �����ϴ�.");
        }
    }

    public List<Skill> GetSkills()
    {
        return skills;
    }

    public void ApplySkill(Skill skill, WeaponHandler weapon)
    {
        Debug.Log($" ApplySkill() ȣ���! {skill.name} ���� ��...");
        if (weapon == null)
        {
            Debug.LogError(" WeaponHandler�� �����ϴ�!");
            return;
        }
        switch (skill.type)
        {
            case "Attack":
                weapon.Power += skill.value;
                Debug.Log($"���ݷ�{weapon.Power}");
                break;
            case "Speed":
                weapon.Speed += skill.value;
                Debug.Log($"���� �ӵ� ����! ���� �ӵ�: {weapon.Speed}");
                break;
            case "Delay":
                weapon.Delay -= skill.value;
                Debug.Log($"���� ������ ����! ���� ������: {weapon.Delay}");
                break;
        }
        Debug.Log($"��ų ����: {skill.name} ({skill.type} +{skill.value})");
    }
}
