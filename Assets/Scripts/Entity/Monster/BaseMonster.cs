using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : Character
{
    protected GameObject Target;
    protected bool TargetFollowMode { get; set; }
    
    protected float AttackCoolDown;
    public override void Attack()
    {
        base.Attack();
    }

    protected virtual void Start()
    {

    }

    /// <summary>
    /// ������ �Ծ��� �� ó�� (�ִϸ��̼� ��)
    /// </summary>
    public override void Damage()
    {
        
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
        transform.position += Direction * (0.1f * MoveSpeed);
    }
}
