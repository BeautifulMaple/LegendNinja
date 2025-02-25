using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; //��ֹ� �迭
    public int obstacleCount = 5; //������ ��ֹ� ����
    public Vector2 mapSize = new Vector2(10, 10); // �� ũ��
    public LayerMask obstacleLayer; //üũ��

    private List<Vector2> usedPositions = new List<Vector2>(); //�̹� ��ġ�� ��ġ ����

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        List<GameObject> availableObstacles = new List<GameObject>(obstaclePrefabs); //��ֹ� ����Ʈ ����

        for (int i = 0; i < obstacleCount && availableObstacles.Count > 0; i++)
        {
            Vector2 randomPos;
            int maxAttempts = 10;

            //��ֹ��� ��ġ�� �ʵ��� ��ġ ã��
            do
            {
                randomPos = new Vector2(
                    Random.Range(-mapSize.x / 2, mapSize.x / 2),
                    Random.Range(-mapSize.y / 2, mapSize.y / 2)
                );
                maxAttempts--;
            } while (usedPositions.Contains(randomPos) || Physics2D.OverlapCircle(randomPos, 0.5f, obstacleLayer) && maxAttempts > 0);

            if (availableObstacles.Count > 0)
            {
                int randomIndex = Random.Range(0, availableObstacles.Count);
                GameObject obstacle = Instantiate(availableObstacles[randomIndex], randomPos, Quaternion.identity);
                availableObstacles.RemoveAt(randomIndex); //����� ��ֹ� ����(�ߺ� ����)
                usedPositions.Add(randomPos);
            }
        }
    }
}
