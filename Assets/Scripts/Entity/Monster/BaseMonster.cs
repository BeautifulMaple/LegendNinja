using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : Character
{
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    protected GameObject Target;
    protected Animator monsterAnimator;
    protected MonsterData myData;
    protected bool TargetFollowMode { get; set; }
    protected float AttackCoolDown;

    private void Awake()
    {
        //monsterAnimator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        AttackCoolDown -= Time.deltaTime;
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

    private void FixedUpdate()
    {
        if(TargetFollowMode == true)
        {
            MoveToTarget();
        }
    }

    /// <summary>
    /// �÷��̾� �������� �� ó��
    /// </summary>
    /// <param name="player">������ Ÿ��(�÷��̾�)</param>
    protected void PlayerDetectStart(GameObject player)
    {
        Target = player;
        TargetFollowMode = true;

        // TO DO : �ִϸ��̼� Move ó��
    }

    /// <summary>
    /// �÷��̾� ���� ���� ó��
    /// </summary>
    protected void PlayerDetectEnd()
    {
        Target = null;
        TargetFollowMode = false;

        // TO DO : �ִϸ��̼� Idle ó��
    }
    public void MoveToTarget()
    {
        if(Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
        {
            Attack();
            return;
        }

        Vector3 Direction = (Target.transform.position - transform.position).normalized;
        transform.position += Direction * (0.05f * MoveSpeed);
        // �ִϸ��̼� ���� ����
        monsterAnimator.SetFloat(MoveX, Direction.x);
        monsterAnimator.SetFloat(MoveY, Direction.y);
    }
}
