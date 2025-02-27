using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer; // ���� �� �浹 ������ ���̾� ����ũ
    public float MyPower { get; set; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �� ������Ʈ �浹 �� ����
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            Destroy(this.gameObject);
        }

        // �÷��̾� �浹 �� Ÿ�� �� ����
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Damage(MyPower);
            Destroy(this.gameObject);
        }
    }
}
