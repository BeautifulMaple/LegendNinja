using Newtonsoft.Json;
using PublicDefinitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

#region ���� ������ Ʋ
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
    public EAttackType type;
    public StatData stats;
}

public class MonsterDatabase
{
    public List<MonsterData> Small;
    public List<MonsterData> Medium;
    public MonsterData Boss;
}
#endregion

#region ���̺� ������ Ʋ
public class WaveData
{ 
    public int wave;
    public int smallType;
    public int mediumType;
    public int smallCount;
    public int mediumCount;
    public int bossType;
}
public class WaveDatabase
{
    public List<WaveData> WaveDatas;
}
#endregion

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

        Debug.Log("MonsterTable �ε� �Ϸ�");
        return MonsterDB;
    }

    public static WaveDatabase LoadWaveData(string jsonFileName)
    {
        TextAsset JsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (JsonFile == null)
        {
            Debug.Log("json ������ null�Դϴ�.");
        }

        WaveDatabase WaveDB = JsonConvert.DeserializeObject<WaveDatabase>(JsonFile.text);

        Debug.Log("WaveDataTable �ε� �Ϸ�");
        return WaveDB;
    }
}
