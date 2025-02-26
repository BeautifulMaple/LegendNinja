using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public int totalWaves = 5; // �� ���̺� ��

    [Header("���� ��ũ��Ʈ ����")]
    public MonsterSpawner monsterSpawner; //���� ���� ���� ��ũ��Ʈ
    public ObstacleSpawner obstacleSpawner; //��ֹ� ���� ���� ��ũ��Ʈ
    public WavePortal wavePortal; //��Ż ��ũ��Ʈ (���� ���̺� ���� Ʈ����)

    private int currentWave = 0;
    private bool waveCleared = false;

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        if (currentWave >= totalWaves)
        {         
            return;
        }

        currentWave++;
        waveCleared = false; // �� ���̺� ����

        int obstacleCount = Mathf.Clamp(3 + (currentWave - 1) * 2, 3, 9); //���̺긶�� ��ֹ� ����
        //���� �� ����       

        obstacleSpawner.ClearObstacles(); //���� ��ֹ� ����
        obstacleSpawner.SpawnObstacles(obstacleCount); //���ο� ��ֹ� ����     
    }

    public void CheckWaveClear()
    {
        if (monsterSpawner.GetAliveEnemyCount() == 0) //���� ���� ������ ���̺� Ŭ����
        {
            Debug.Log($"���̺� {currentWave} Ŭ����! ��Ż�� �̵��ϼ���.");
            waveCleared = true;
            wavePortal.ActivatePortal(); //��Ż Ȱ��ȭ
        }
    }

    public void TryStartNextWave()
    {
        if (waveCleared)
        {
            StartNextWave();
        }
    }
}
