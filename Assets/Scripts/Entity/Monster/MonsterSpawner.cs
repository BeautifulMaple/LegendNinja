using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private int curWave = 2; // ���� wave ��. ���߿� �Ŵ������� �������� wave������ ��ü�� �� ����.

    private MonsterDatabase monsterDB;
    private WaveDatabase waveDB;

    void Start()
    {
        monsterDB = DataTableLoader.LoadMonsterData("MonsterTable");
        waveDB = DataTableLoader.LoadWaveData("WaveDataTable");

        WaveSpawn();
    }
    /// <summary>
    /// ���� wave�� �´� ���͸� ���� ��ȯ.
    /// </summary>
    void WaveSpawn()
    {
        WaveData waveData = waveDB.WaveDatas[curWave - 1]; // ���� ���̺� ������

        // ���� ���� ���� �̱�
        List<MonsterData> small = monsterDB.Small.OrderBy(x => Random.value).Take(waveData.smallType).ToList(); // smallType�� ���� �̱�   
        for(int i = 0; i < waveData.smallCount; i++)    // �� �� smallCount���� ��ȯ
        {
            int idx = Random.Range(0, waveData.smallType);
            Spawn(small[idx]);

            Debug.Log($"���� ��ȯ | �ε��� : {idx}, id : {small[idx].id}");
        }

        // ���� ���� ���� �̱�
        List<MonsterData> mid = monsterDB.Medium.OrderBy(x => Random.value).Take(waveData.mediumType).ToList(); // {mediumType} ���� �̱�   
        for (int i = 0; i < waveData.mediumCount; i++)  // �� �� mediumCount���� ��ȯ
        {
            int idx = Random.Range(0, waveData.mediumType);
            Spawn(mid[idx]);

            Debug.Log($"���� ��ȯ | �ε��� : {idx}, id : {mid[idx].id}");
        }

        // ���� �̱� 
        if (waveData.bossType > 0)
        {
            MonsterData boss = monsterDB.Boss.OrderBy(x => Random.value).Take(waveData.bossType).First<MonsterData>();
            Spawn(boss);

            Debug.Log($"���� ��ȯ | id : {boss.id}");
        }
    }

    void Spawn(MonsterData data)
    {
        GameObject go = Resources.Load<GameObject>($"Prefab/Monster/{data.id}");
        if (go == null) return;

        Instantiate(go).GetComponent<Monster>().InitMonster(data);
    }
}
