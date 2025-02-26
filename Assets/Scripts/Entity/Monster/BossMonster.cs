using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : BaseMonster
{
    protected static readonly int IsHidden = Animator.StringToHash("IsHidden");

    private SpriteRenderer bossRenderer;
    private bool IsBossSkillOn = false;

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
        TargetPlayer.Damage(AttackPower);
    }
    public override void MoveToTarget()
    {
        if (IsBossSkillOn)
        {
            BossSkill();
            return;
        }

        if (Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
        {
            // Ȯ�������� Hidden ��ų ���
            int p = Random.Range(0, 100);
            if (p > 80)
            {
                IsBossSkillOn = true;
                return;
            }
            // �⺻ ����
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

    void BossSkill()
    {
        monsterAnimator.SetBool(IsHidden, true);
        Debug.Log("���� ��ų �ߵ�");
        // TO DO : ���� ��ġ�� �̵�
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
