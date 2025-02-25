using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BaseMonster
{
    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        AttackCoolDown -= Time.deltaTime;
    }
    public override void Attack()
    {
        if (AttackCoolDown > 0f) return;

        base.Attack();
        Debug.Log("Melee Attack");
        AttackCoolDown = AttackTime;
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
