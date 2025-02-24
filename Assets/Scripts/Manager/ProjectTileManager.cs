using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTileManager : MonoBehaviour
{
    private static ProjectTileManager instance;
    public static ProjectTileManager Instance { get { return instance; } }
    [SerializeField] private GameObject[] projectTilePrefabs;

    private void Awake()
    {
        instance = this;
    }

    public void ShootBullet( Vector2 startPosition, Vector2 direction)
    {

    }
}
