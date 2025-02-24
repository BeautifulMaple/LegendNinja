using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StatData
{
    public float MaxHealth;
    public float AttackPower;
    public float Defense;
    public float MoveSpeed;
    public float AttackRange;
    public float AttackSpeed;
    public float AttackTime;
}

public class MonsterData
{
    public int id;
    public string name;
    public StatData stats;
}

public class MonsterDatabase
{
    public List<MonsterData> Small;
    public List<MonsterData> Medium;
    public MonsterData Boss;
}

public static class DataTableLoader
{
    /// <summary>
    /// ���������� ���� �����͸� �ε��ϴ� �Լ�
    /// </summary>
    /// <param name="jsonFileName">�о�� json ���ϸ�</param>
    /// <returns>������ ���Ĵ�� �����͸� ��ȯ</returns>
    public static MonsterDatabase LoadMonsterData(string jsonFileName)
    {
        TextAsset monsterJsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (monsterJsonFile == null)
        {
            Debug.Log("json ������ null�Դϴ�.");
        }

        MonsterDatabase MonsterDB = JsonConvert.DeserializeObject<MonsterDatabase>(monsterJsonFile.text);

        //foreach (MonsterData monster in MonsterDB.Small)
        //{
        //    Debug.Log($"���� ����: {monster.name}, ���ݷ�: {monster.stats.AttackPower}");
        //}
        //foreach (MonsterData monster in MonsterDB.Medium)
        //{
        //    Debug.Log($"���� ����: {monster.name}, ���ݷ�: {monster.stats.AttackPower}");
        //}
        //Debug.Log($"���� : {MonsterDB.Boss.name}");

        return MonsterDB;
    }
}
