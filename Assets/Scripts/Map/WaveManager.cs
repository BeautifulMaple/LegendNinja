using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Runtime.CompilerServices;

public class WaveManager : MonoBehaviour
{

    public static WaveManager instance { get; private set; }
    public int totalWaves = 5; // �� ���̺� ��

    [Header("���� ��ũ��Ʈ ����")]
    public MonsterSpawner monsterSpawner; //���� ���� ���� ��ũ��Ʈ
    public ObstacleSpawner obstacleSpawner; //��ֹ� ���� ���� ��ũ��Ʈ
    public WavePortal wavePortal; //��Ż ��ũ��Ʈ (���� ���̺� ���� Ʈ����)
    

    public int AliveEnemyCount {  get; set; }
    public int CurrentWave {  get; set; }

    private bool waveCleared = false;
    private List<Vector2> spawnedPosition;
    public Vector2 mapSize = new Vector2(10, 10); //��ũ��


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (CurrentWave >= totalWaves)
        {         
            return;
        }

        CurrentWave++;
        waveCleared = false; // �� ���̺� ����

        int obstacleCount = Mathf.Clamp(3 + (CurrentWave - 1) * 2, 3, 9); //���̺긶�� ��ֹ� ����
        //���� �� ����       

        obstacleSpawner.ClearObstacles(); //���� ��ֹ� ����

        spawnedPosition = new List<Vector2>(); //������ ��ġ ����Ʈ

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
    public Vector2 GetRandomPosition()
    {
        float x = Random.Range(-mapSize.x / 2 ,mapSize.y / 2);
        float y = Random.Range(-mapSize.y /2 ,mapSize.x / 2);
        return new Vector2(x, y);
    }

    //��ֹ� ��ġ ��ġ���� Ȯ��
    private bool IsPositionOccupied(Vector2 position,List<Vector2> _spawnedPosition)
    {
        foreach (Vector2 spawnedPosiotion in _spawnedPosition)
        {
            if (Vector2.Distance(position, spawnedPosiotion) < 1f)
            {
                return true;
            }
        }

        return false;
    }

    //��ֹ� ��ġ ��ġ���� Ȯ�� (�ܺο�)
    public bool IsPositionOccupied(Vector2 position)
    {
        if (spawnedPosition == null) return true;
        foreach (Vector2 sPos in spawnedPosition)
        {
            if (Vector2.Distance(position, sPos) < 1f)
            {
                return true;
            }
        }

        return false;
    }

    public void CheckWaveClear()
    {
        if (AliveEnemyCount == 0) //���� ���� ������ ���̺� Ŭ����
        {
            Debug.Log($"���̺� {CurrentWave} Ŭ����! ��Ż�� �̵��ϼ���.");
            waveCleared = true;
            //wavePortal.ActivatePortal(); //��Ż Ȱ��ȭ
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
