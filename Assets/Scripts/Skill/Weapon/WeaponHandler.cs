using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponHandler : MonoBehaviour
{
    //  ���� ���� ���� ����
    [Header("Attack Info")]

    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }
    // ���� ����(��Ÿ��) - ���Ⱑ ������ ������ ���� �ð� ������ ����

    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }
    // ���� ũ�� - ���� ���� ������ ������ �� �� ����

    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }
    // ���ݷ� - ������ ���ϴ� ���ط��� ����

    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }
    // ���� �ӵ� - �߻�ü�� ���ư��� �ӵ�

    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange = value; }
    // ���� ���� - ���Ÿ� ������ ���, �ִ� ��Ÿ��� �ǹ�

    public LayerMask target;
    // ���� ��� ���� - Ư�� Layer(��, ���� ��)�� ������ �� �ֵ��� ����

    //  �˹�(���� ���ĳ��� ȿ��) ���� ����
    [Header("Knock Back Info")]

    [SerializeField] private bool isOnknockBack = false;
    public bool IsOnknockBack { get => isOnknockBack; set => isOnknockBack = value; }
    // �˹� ȿ�� ���� ���� - true�� ���� ������ �¾��� �� �з���

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
    // �˹� ���� - ���� �󸶳� ���ϰ� ���ĳ��� ����

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }
    // �˹� ���� �ð� - ���� �˹� ���¿� �ӹ��� �ð�

    //  �ִϸ��̼� ���� �ؽð� (���� �ִϸ��̼� Ʈ����)
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    // �ִϸ����Ϳ��� "IsAttack" �Ķ���͸� �ؽ� ������ �����Ͽ� ���� ����ȭ


    // Animator ������Ʈ ����
    private Animator animator;
    // SpriteRenderer ������Ʈ ����
    private SpriteRenderer weaponRenderer;

    protected virtual void Start()
    {

    }

    // �ʱ�ȭ �޼���
    protected virtual void Awake()
    {
        // �θ� ��ü���� Animator ������Ʈ ��������
        animator = GetComponentInParent<Animator>();
        // �ڽ� ��ü���� SpriteRenderer ������Ʈ ��������
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        // �ִϸ����� �ӵ� ����
        animator.speed = 1.0f / delay;
        // ���� ũ�� ����
        transform.localScale = Vector3.one * weaponSize;
    }

    // ���� �޼���
    public virtual void Attack()
    {
        AttackAnimation();
    }

    // ���� �ִϸ��̼� Ʈ���� �޼���
    private void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    // ���� ȸ�� �޼���
    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
