using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : BaseMonster
{
    private SpriteRenderer bossRenderer;
    private void Awake()
    {
        monsterAnimator = GetComponentInChildren<Animator>();
        GetComponentInChildren<CircleCollider2D>().radius = 6f;
        bossRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public override void Attack()
    {
        base.Attack();

        if (AttackCoolDown > 0f) return;
        AttackCoolDown = AttackTime;
    }
    public override void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
        {
            Attack();
            return;
        }

        // Move
        TargetDir = (Target.transform.position - transform.position).normalized;
        transform.position += TargetDir * (0.05f * MoveSpeed);

        // �ִϸ��̼� ���� & ���� ����
        monsterAnimator.SetBool(IsAttack, false);
        monsterAnimator.SetBool(IsMoving, true);

        if(TargetDir.x < 0)
        {
            bossRenderer.flipX = true;
        }
        else
        {
            bossRenderer.flipX = false;
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
}
