using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Runtime.CompilerServices;

public class WaveManager : MonoBehaviour
{
    public int totalWaves = 5; // �� ���̺� ��

    [Header("���� ��ũ��Ʈ ����")]
    public MonsterSpawner monsterSpawner; //���� ���� ���� ��ũ��Ʈ
    public ObstacleSpawner obstacleSpawner; //��ֹ� ���� ���� ��ũ��Ʈ
    public WavePortal wavePortal; //��Ż ��ũ��Ʈ (���� ���̺� ���� Ʈ����)

    private int currentWave = 0;
    private bool waveCleared = false;
    public Vector2 mapSize = new Vector2(10, 10); //��ũ��

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

        List<Vector2> spawnedPosition = new List<Vector2>(); //������ ��ġ ����Ʈ

        for (int i = 0; i < obstacleCount; i++)
        {
            Vector2 randomPosition = GetRandomPosition();
            while (IsPositionOccupied(randomPosition,spawnedPosition))
            {
                randomPosition = GetRandomPosition();
            }
           
            int randomIndex = Random.Range(0, obstacleSpawner.obstaclePrefabs.Length);
            obstacleSpawner.SpawnObstacles(randomPosition, randomIndex);

            spawnedPosition.Add(randomPosition);
        }
        
    }

    //������ġ����
    Vector2 GenRandomPosition()
    {
        float x = Random.Range(-mapSize.x / 2 ,mapSize.y / 2);
        float y = Random.Range(-mapSize.y /2 ,mapSize.x / 2);
        return new Vector2(x, y);
    }

    //��ֹ� ��ġ ��ġ���� Ȯ��
    bool IsPositionOccupied(Vector2 position,List<Vector2> spawnedPosition)
    {
        foreach (Vector2 spawnedPosiotion in spawnedPosition)
        {
            if (Vector2.Distance(position, spawnedPosiotion) < 1f)
            {
                return true;
            }
        }
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
