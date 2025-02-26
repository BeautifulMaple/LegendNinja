using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : Character
{
    protected static readonly int MoveX = Animator.StringToHash("MoveX");
    protected static readonly int MoveY = Animator.StringToHash("MoveY");
    protected static readonly int IsMoving = Animator.StringToHash("IsMoving");
    protected static readonly int IsAttack = Animator.StringToHash("IsAttack");

    protected GameObject Target;
    protected Player TargetPlayer;
    protected Animator monsterAnimator;
    protected MonsterData myData;
    protected SpriteRenderer monsterRenderer;

    protected bool TargetFollowMode { get; set; }
    protected float AttackCoolDown { get; set; }    // ���� ��Ÿ��, stat���� AttackTime�� ���.
    protected Vector3 TargetDir { get; set; }

    private Color originalColor;
    public abstract void MoveToTarget();

    protected virtual void Awake()
    {
        monsterRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = monsterRenderer.color;
    }
    protected virtual void Update()
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
        monsterAnimator.SetBool(IsMoving, false);
        monsterAnimator.SetBool(IsAttack, true);
    }

    /// <summary>
    /// �ǰ� ó�� (Health ����, �ִϸ��̼�)
    /// </summary>
    /// <param name="damage">�ǰ� ������ ũ��</param>
    public override void Damage(float damage)
    {
        Health -= damage;
        monsterRenderer.color = monsterRenderer.color - new Color(0, 0.7f, 0.7f, 0f);
        Invoke("ResetColor", 0.3f);
        if (Health <= 0)
        {
            TargetFollowMode = false;
            monsterRenderer.color = monsterRenderer.color - new Color(0f, 1f, 1f, 0.4f);
            monsterAnimator.speed = 0f;
            gameObject.tag = "Default";
            GetComponent<Collider2D>().enabled = false;
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
        
        monsterAnimator.SetBool(IsMoving, false);
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

    void ResetColor()
    {
        monsterRenderer.color = originalColor;
    }
}
