using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData

{
    public int id;
    public string name;
    public string type;
    public float value;
    public string description;
    public string sprite;

    // ���� �Ӽ� �߰�
    public float damage;    // �ִ� ���ݷ�
    public float speed;     // ���ư��� �ӵ�
    public float cooldown;  // ��Ÿ��
    public int bulletIndex; // ź �ε���
    public float bulletSize; // ź ������
    public float duration;  // �����ð�
    public float spread;    // ���� ����ȭ
    public float numberofProjectilesPerShot;    // ź ����
    public float multipleProjectilesAngel;  //ź ����

    public string weaponPrefab;
}

[Serializable]
public class SkillList
{
    public SkillData[] skills;
}
