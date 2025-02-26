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
                foreach(WeaponHandler weaponHandler in player.weaponList)
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
                player.weaponList[0].NumberofProjectilesPerShot += skill.value;
                player.weaponList[0].MultipleProjectilesAngel += 10;
                break;
            case "Fire":
            case "Ice":
            case "Thunder":
            case "Plant":
            case "Rock":
                    AddweaponList(skill); 
                break;
        }

        Debug.Log($"{skill.name} �����! ���� Power: {player.weaponList[0].Damage}, Speed: {player.weaponList[0].AttackSpeed}");
    }

    public void AddweaponList(SkillData skill)
    {
        if(string.IsNullOrEmpty(skill.weaponPrefab))
        {
            Debug.LogError($"���� �������� �������� ���� : {skill.name}");
            return;
        }
        GameObject weaponPrefab = Resources.Load<GameObject>(skill.weaponPrefab);
        if (weaponPrefab == null)
        {
            Debug.LogError($"���� �������� ã�� �� ����: {skill.weaponPrefab}");
            return;
        }
        GameObject newWeapon = Instantiate(weaponPrefab, player.PlayerPivot.transform);
        RangeWeaponHandler newWeaponHandler = newWeapon.GetComponent<RangeWeaponHandler>();
        //player.weaponList.Add(new RangeWeaponHandler(player.PlayerPivot.transform, bulletIndex, 1, 5, 0, 1, 10, weaponColor, ProjectileManager.Instance));
        
        if(newWeaponHandler != null)
        {
            player.weaponList.Add(newWeaponHandler);

            // ��ų ������ �ݿ��ϱ�
            newWeaponHandler.Damage = skill.damage;
            newWeaponHandler.AttackSpeed = skill.speed;
            newWeaponHandler.Delay = skill.cooldown;
            //newWeaponHandler.Type = skill.type;

            Debug.Log($"{skill.name} ���� �߰���! (������: {skill.damage}, �ӵ�: {skill.speed}, ��Ÿ��: {skill.cooldown})");
        }
        else Debug.LogError("RangeWeaponHandler�� �����տ� ����!");

    }
    public void AttackWithWeapons(Vector3 direction)
    {
        foreach (WeaponHandler weaponHandler in player.weaponList)
        {
            weaponHandler.Attack(direction);
        }
    }


}
