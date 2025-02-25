using PublicDefinitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BaseMonster
{
    private void Awake()
    {
        monsterAnimator = GetComponentInChildren<Animator>();
    }

    public override void Attack()
    {
        base.Attack();
        switch (myData.type)
        {
            case EAttackType.Melee:
                break;
            case EAttackType.Ranged:
                break;
            case EAttackType.AoE:
                break;
            default:
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾� ������
            PlayerDetectStart(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾� ���� ����
            PlayerDetectEnd();
        }
    }

    /// <summary>
    /// data�� stat �ʱ�ȭ
    /// </summary>
    /// <param name="data">���� ������</param>
    public void InitMonster(MonsterData data)
    {
        myData = data;

        Health = MaxHealth = data.stats.MaxHealth;
        AttackPower = data.stats.AttackPower;
        Defense = data.stats.Defense;
        MoveSpeed = data.stats.MoveSpeed;
        AttackRange = data.stats.AttackRange;
        AttackSpeed = data.stats.AttackSpeed;
        AttackTime = data.stats.AttackTime;
    }
}
