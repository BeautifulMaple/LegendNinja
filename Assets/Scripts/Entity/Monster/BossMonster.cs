using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : BaseMonster
{
    protected static readonly int IsHidden = Animator.StringToHash("IsHidden");

    [SerializeField] private LayerMask obstacleLayer;

    private readonly float SkillFullTime = 5f;
    private bool isBossSkillOn = false;
    private float skillRuntime = 5f;

    protected override void Awake()
    {
        base.Awake();

        monsterAnimator = GetComponentInChildren<Animator>();
        GetComponentInChildren<CircleCollider2D>().radius = 8f;
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
        if (isBossSkillOn)
        {
            skillRuntime -= Time.deltaTime;
            if (skillRuntime < 0f)
            {
                BossSkillEnd();
                return;
            }
            BossSkill();
            return;
        }

        if (Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
        {
            // Ȯ�������� Hidden ��ų ���
            int p = Random.Range(0, 100);
            if (p > 80)
            {
                isBossSkillOn = true;
                BossSkillStart();
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
            monsterRenderer.flipX = true;
        }
        else
        {
            monsterRenderer.flipX = false;
        }
    }

    /// <summary>
    /// ���� ��ų ù ��ŸƮ (���� ��ġ�� �̵�, �ִϸ��̼� ��ȯ)
    /// </summary>
    void BossSkillStart()
    {
        monsterAnimator.SetBool(IsHidden, true);
        skillRuntime = SkillFullTime;
        gameObject.tag = "Untagged";
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// ���� ��ų ����ϴ� ���� ����
    /// </summary>
    void BossSkill()
    {
        monsterRenderer.color = monsterRenderer.color - new Color(0, 0, 0, Time.deltaTime * 0.8f);
    }

    /// <summary>
    /// ���� ��ų ������ ����� �ϴ� �͵�
    /// </summary>
    void BossSkillEnd()
    {
        isBossSkillOn = false;
        monsterRenderer.color = originalColor;
        monsterAnimator.SetBool(IsHidden, false);
        GetComponent<Collider2D>().enabled = true;
        gameObject.tag = "Monster";

        // ���� ��ġ�� �̵�
        Vector3 randomPos = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        while (Physics2D.OverlapCircle(randomPos, 0.5f, obstacleLayer) != null)
        {
            randomPos.x = Random.Range(-3f, 3f);
            randomPos.y = Random.Range(-3f, 3f);
        }
        transform.position = randomPos;
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
