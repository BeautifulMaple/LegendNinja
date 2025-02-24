using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private int stage = 1;
    private int curWave = 1;

    private string fileName = "";
    private MonsterDatabase monsterDB;

    void Start()
    {
        fileName = $"Stage{stage}_MonsterTable";
        monsterDB = DataTableLoader.LoadMonsterData(fileName);
    }   

    //void RandomSpawn(int sNum, int mNum)
    //{
 
    //}
}
