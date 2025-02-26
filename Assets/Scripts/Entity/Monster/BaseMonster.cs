using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : Character
{
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    protected GameObject Target;
    protected Player TargetPlayer;
    protected Animator monsterAnimator;
    protected MonsterData myData;
    protected bool TargetFollowMode { get; set; }
    protected float AttackCoolDown { get; set; }
    protected Vector3 TargetDir { get; set; }

    private void Awake()
    {
        
    }
    private void Update()
    {
        AttackCoolDown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(TargetFollowMode == true)
        {
            MoveToTarget();
        }
    }

    public override void Attack()
    {
        base.Attack();
        if (AttackCoolDown > 0f) return;
        AttackCoolDown = AttackTime;
    }

    /// <summary>
    /// �ǰ� ó�� (Health ����, �ִϸ��̼�)
    /// </summary>
    /// <param name="damage">�ǰ� ������ ũ��</param>
    public override void Damage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            // TO DO : �ִϸ��̼� Die ó��
            Destroy(this.gameObject, 1f);
        }
    }

    /// <summary>
    /// �÷��̾� �������� �� ó��
    /// </summary>
    /// <param name="player">������ Ÿ��(�÷��̾�)</param>
    protected void PlayerDetectStart(GameObject player)
    {
        Target = player;
        TargetPlayer = player.GetComponent<Player>();
        TargetFollowMode = true;
    }

    /// <summary>
    /// �÷��̾� ���� ���� ó��
    /// </summary>
    protected void PlayerDetectEnd()
    {
        Target = null;
        TargetFollowMode = false;
    }
    public void MoveToTarget()
    {
        if(Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
        {
            Attack();
            return;
        }

        TargetDir = (Target.transform.position - transform.position).normalized;
        transform.position += TargetDir * (0.05f * MoveSpeed);
        // �ִϸ��̼� ���� ����
        monsterAnimator.SetFloat(MoveX, TargetDir.x);
        monsterAnimator.SetFloat(MoveY, TargetDir.y);
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
