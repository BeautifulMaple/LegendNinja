using PublicDefinitions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private MonsterDatabase monsterDB;
    private WaveDatabase waveDB;
    private WaveManager waveManager;

    void Start()
    {
        monsterDB = DataTableLoader.LoadMonsterData("MonsterTable");
        waveDB = DataTableLoader.LoadWaveData("WaveDataTable");
        waveManager = WaveManager.instance;

        WaveSpawn();
    }
    /// <summary>
    /// ���� wave�� �´� ���͸� ���� ��ȯ.
    /// </summary>
    void WaveSpawn()
    {
        WaveData waveData = waveDB.WaveDatas[waveManager.CurrentWave - 1]; // ���� ���̺� ������
        Spawn(monsterDB.Small[0]); // 101���� �׽�Ʈ�� �ڵ�
        Spawn(monsterDB.Small[1]); // 102���� �׽�Ʈ�� �ڵ�
        Spawn(monsterDB.Small[2]); // 103���� �׽�Ʈ�� �ڵ�
        Spawn(monsterDB.Boss[0]); // 301���� �׽�Ʈ�� �ڵ�

        //// ���� ���� ���� �̱�
        //List<MonsterData> small = monsterDB.Small.OrderBy(x => Random.value).Take(waveData.smallType).ToList(); // smallType�� ���� �̱�   
        //for(int i = 0; i < waveData.smallCount; i++)    // �� �� smallCount���� ��ȯ
        //{
        //    int idx = Random.Range(0, waveData.smallType);
        //    //Spawn(small[idx]); 

        //    Debug.Log($"���� ��ȯ | �ε��� : {idx}, id : {small[idx].id}");
        //}

        //// ���� ���� ���� �̱�
        //List<MonsterData> mid = monsterDB.Medium.OrderBy(x => Random.value).Take(waveData.mediumType).ToList(); // {mediumType} ���� �̱�   
        //for (int i = 0; i < waveData.mediumCount; i++)  // �� �� mediumCount���� ��ȯ
        //{
        //    int idx = Random.Range(0, waveData.mediumType);
        //    Spawn(mid[idx]);

        //    Debug.Log($"���� ��ȯ | �ε��� : {idx}, id : {mid[idx].id}");
        //}

        //// ���� �̱� 
        //if (waveData.bossType > 0)
        //{
        //    MonsterData boss = monsterDB.Boss.OrderBy(x => Random.value).Take(waveData.bossType).First<MonsterData>();
        //    Spawn(boss);

        //    Debug.Log($"���� ��ȯ | id : {boss.id}");
        //}
    }

    void Spawn(MonsterData data)
    {
        GameObject go = Resources.Load<GameObject>($"Prefab/Monster/{data.id}");
        if (go == null) return;

        Vector3 randomPos = waveManager.GetRandomPosition();
        while (!waveManager.IsPositionOccupied(randomPos))
        {
            randomPos = waveManager.GetRandomPosition();
        }

        if (data.type == EAttackType.Melee)
        { 
            Instantiate(go, randomPos, Quaternion.identity).AddComponent<MeleeMonster>().InitMonster(data); 
        }
        else if (data.type == EAttackType.Ranged)
        {
            Instantiate(go, randomPos, Quaternion.identity).AddComponent<RangedMonster>().InitMonster(data);
        }
        else if (data.id >= 300)
        {
            Instantiate(go, randomPos, Quaternion.identity).AddComponent<BossMonster>().InitMonster(data);
        }

        waveManager.AliveEnemyCount++;
    }
}
