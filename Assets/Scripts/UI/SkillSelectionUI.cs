using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillData;

public class SkillSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private TextMeshProUGUI skillDescription;

    private SkillManager skillManager;
    private WeaponHandler playerWeapon;
    private List<Skill> availableSkills; // SkillData �� Skill �� ����

    private void Start()
    {
        skillManager = SkillManager.Instance;
        if (skillManager == null)
        {
            Debug.LogError(" SkillManager.Instance�� NULL�Դϴ�! SkillManager�� ���� �ִ��� Ȯ���ϼ���.");
        }

        playerWeapon = FindObjectOfType<WeaponHandler>();

        if (playerWeapon == null)
        {
            Debug.LogError(" WeaponHandler�� ������ �߰ߵ��� �ʾҽ��ϴ�!");
        }

        panel.SetActive(false);
    }

    public void ShowSkillSelection()
    {
        Debug.Log(" ShowSkillSelection() ȣ���!");

        if (skillManager == null)
        {
            Debug.LogError(" skillManager�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        availableSkills = skillManager.GetSkills();

        if (availableSkills == null || availableSkills.Count == 0)
        {
            Debug.LogError(" ��ų �����Ͱ� ��� �ֽ��ϴ�! JSON ������ Ȯ���ϼ���.");
            return;
        }

        panel.SetActive(true);
        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i] == null)
            {
                Debug.LogError($" skillButtons[{i}]��(��) null�Դϴ�! Unity���� ��ư�� �Ҵ��ߴ��� Ȯ���ϼ���.");
                continue;
            }

            if (i < availableSkills.Count)
            {
                Skill skill = availableSkills[i];

                TMP_Text buttonText = skillButtons[i].GetComponentInChildren<TMP_Text>(); // Text -> TMP_Text
                if (buttonText == null)
                {
                    Debug.LogError($" skillButtons[{i}]�� TMP_Text ������Ʈ�� �������� �ʽ��ϴ�!");
                    continue;
                }

                buttonText.text = skill.name;
            }
        }

    }


    public void SelectSkill(Skill skill)
    {
        Debug.Log($" SelectSkill() ȣ���! ���õ� ��ų: {skill.name} ({skill.type} +{skill.value})");

        skillManager.ApplySkill(skill, playerWeapon);
        skillDescription.text = $"������ ��ų: {skill.name} ({skill.type} +{skill.value})";
        //  ���� ���ݷ� Ȯ�ο� ����� �α� �߰�
        Debug.Log($"[Skill Applied] {skill.name} �����! ���� ���ݷ�: {playerWeapon.Power}");
        panel.SetActive(false);
    }
}
