using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : BaseMonster
{
    protected static readonly int IsHidden = Animator.StringToHash("IsHidden");
    protected static readonly int IsDeath = Animator.StringToHash("IsDeath");

    [SerializeField] private LayerMask obstacleLayer;

    private readonly float SkillFullTime = 3f;
    private bool isBossSkillOn = false;
    private float skillRuntime = 3f;
    private WaveManager waveManager;

    protected override void Awake()
    {
        base.Awake();

        monsterAnimator = GetComponentInChildren<Animator>();
        GetComponentInChildren<CircleCollider2D>().radius = 8f;
    }

    private void Start()
    {
        waveManager = WaveManager.instance;
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if(Health <= 0)
        {
            TargetFollowMode = false;
            monsterAnimator.SetBool(IsDeath, true);
            gameObject.tag = "Untagged";
            GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject, 3f);
        }
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
            float p = Random.Range(0f, 100f);
            if (p < 1f)
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
        monsterAnimator.SetBool(IsMoving, false);
        monsterAnimator.SetBool(IsAttack, false);
        monsterAnimator.SetBool(IsHidden, true);
        gameObject.tag = "Untagged";
        GetComponent<Collider2D>().enabled = false;

        TargetDir = (Target.transform.position - transform.position).normalized;
        if (TargetDir.x < 0)
        {
            monsterRenderer.flipX = true;
        }
        else
        {
            monsterRenderer.flipX = false;
        }
    }

    /// <summary>
    /// ���� ��ų ����ϴ� ���� ����
    /// </summary>
    void BossSkill()
    {
        monsterRenderer.color = monsterRenderer.color - new Color(0, 0, 0, Time.deltaTime * 1.1f);
    }

    /// <summary>
    /// ���� ��ų ������ ����� �ϴ� �͵�
    /// </summary>
    void BossSkillEnd()
    {
        isBossSkillOn = false;
        skillRuntime = SkillFullTime;
        monsterRenderer.color = originalColor;
        monsterAnimator.SetBool(IsHidden, false);
        GetComponent<Collider2D>().enabled = true;
        gameObject.tag = "Monster";

        // ���� ��ġ�� �̵�
        Vector3 randomPos = waveManager.GetRandomPosition();
        while (!waveManager.IsPositionOccupied(randomPos))
        {
            randomPos = waveManager.GetRandomPosition();
        }
        transform.position = randomPos;

        TargetDir = (Target.transform.position - transform.position).normalized;
        if (TargetDir.x < 0)
        {
            monsterRenderer.flipX = true;
        }
        else
        {
            monsterRenderer.flipX = false;
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
