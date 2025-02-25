using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private int stage = 1;
    private int curWave = 5; // ���� wave ��. ���߿� �Ŵ������� �������� wave������ ��ü�� �� ����.

    private MonsterDatabase monsterDB;
    private WaveDatabase waveDB;

    void Start()
    {
        monsterDB = DataTableLoader.LoadMonsterData("MonsterTable");
        waveDB = DataTableLoader.LoadWaveData("WaveDataTable");

        RandomMonsterSpawn();
    }
    /// <summary>
    /// ���� wave�� �´� ���͸� ���� ��ȯ.
    /// </summary>
    void RandomMonsterSpawn()
    {
        WaveData waveData = waveDB.WaveDatas[curWave - 1]; // ���� ���̺� ������

        // ���� ���� ���� �̱�
        List<MonsterData> small = monsterDB.Small.OrderBy(x => Random.value).Take(waveData.smallType).ToList(); // smallType�� ���� �̱�   
        // �� �� smallCount���� ��ȯ
        for(int i = 0; i < waveData.smallCount; i++)
        {
            int idx = Random.Range(0, waveData.smallType);
            GameObject go = Resources.Load<GameObject>($"Prefab/Monster/{small[idx].id}");
            if (go != null) Instantiate(go);

            Debug.Log($"���� ��ȯ | �ε��� : {idx}, id : {small[idx].id}");
        }

        // ���� ���� ���� �̱�
        List<MonsterData> mid = monsterDB.Medium.OrderBy(x => Random.value).Take(waveData.mediumType).ToList(); // {mediumType} ���� �̱�   
        // �� �� mediumCount���� ��ȯ
        for (int i = 0; i < waveData.mediumCount; i++)
        {
            int idx = Random.Range(0, waveData.mediumType);
            GameObject go = Resources.Load<GameObject>($"Prefab/Monster/{mid[idx].id}");
            if (go != null) Instantiate(go);

            Debug.Log($"���� ��ȯ | �ε��� : {idx}, id : {mid[idx].id}");
        }

        if (waveData.bossType > 0)
        {
            // ���� �̱� 
            MonsterData boss = monsterDB.Boss.OrderBy(x => Random.value).Take(waveData.bossType).First<MonsterData>();
            GameObject go = Resources.Load<GameObject>($"Prefab/Monster/{boss.id}");
            if (go != null) Instantiate(go);
            Debug.Log($"���� ��ȯ | id : {boss.id}");
        }
    }
}
