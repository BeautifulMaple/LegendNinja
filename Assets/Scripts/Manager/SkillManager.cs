using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private SkillList skillList;
    public Player player;


    private void Start()
    {
        skillList = LoadSkills();
    }

    private SkillList LoadSkills()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Skills");
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
                foreach (WeaponHandler weaponHandler in player.weaponList)
                {
                    weaponHandler.Damage += skill.value;
                    Debug.Log($"power{weaponHandler.Damage}");
                }
                break;
            case "delay":
                player.AttackMaxCoolDown += skill.value;
                break;
            case "speed":
                foreach (WeaponHandler weaponHandler in player.weaponList)
                {
                    weaponHandler.AttackSpeed += skill.value;
                    Debug.Log($"AttackSpeed{weaponHandler.AttackSpeed}");
                }
                break;
            case "addProjectilesPerShot":
                player.weaponList[0].NumberOfProjectilesPerShot += skill.value;
                player.weaponList[0].MultipleProjectilesAngle += 10;
                break;
            case "Fire":
            case "Ice":
            case "Thunder":
            case "Plant":
            case "Rock":
                RangeWeaponHandler existingWeapon = player.weaponList
                    .FirstOrDefault(w => w.WeaponType == skill.type);

                if (existingWeapon != null)
                {
                    // ���� ��ȭ
                    existingWeapon.Damage += skill.value;
                    Debug.Log($"{skill.name} ��ȭ��! (���ο� Damage: {existingWeapon.Damage})");
                }
                else
                {
                    // ���ο� ���� �߰�
                    AddweaponList(skill);
                }
                break;
        }
    }

    public void AddweaponList(SkillData skill)
    {
        Debug.Log($"{skill.name} ���� �߰� �õ� (ID: {skill.id})");

        player.weaponList.Add(new RangeWeaponHandler(player.PlayerPivot.transform, skill.damage, skill.speed, skill.cooldown,
            skill.bulletIndex, skill.bulletSize,
            skill.duration, skill.spread, skill.numberofProjectilesPerShot, skill.multipleProjectilesAngel, 
            Color.white, ProjectileManager.Instance, skill.type));
        player.AttackCooldwonDivide();

        // �ڷ�ƾ ������ �޼ҵ� ����
        // �÷��̾�� �ڷ�ƾ ��������

        Debug.Log($" {skill.name} ���� �߰���! (������: {skill.damage}, �ӵ�: {skill.speed}, ��Ÿ��: {skill.cooldown})");
    }
   
    public void AttackWithWeapons(Vector3 direction, ref int index, List<RangeWeaponHandler> rangeWeaponHandlers)
    {
        rangeWeaponHandlers[index].Attack(direction);
        index++;

        if(index >= rangeWeaponHandlers.Count)
        {
            index = 0;
        }
    }


}
