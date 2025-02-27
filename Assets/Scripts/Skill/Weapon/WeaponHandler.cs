using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f; // ���� ������ (��)
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1f; // ������ ũ��
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float damage = 10f; // ���ݷ�
    public float Damage { get => damage; set => damage = value; }

    [SerializeField] private float attackSpeed = 10f; // ����ü �ӵ� ����
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }

    [SerializeField] private float attackRange = 10f; // ���� ����
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target; // ���� ��� ���̾� ����

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false; // �˹� ���� ����
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f; // �˹� ��
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f; // �˹� ���� �ð�
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }


    // �� ���⸦ ������ BaseController (�÷��̾ �� ĳ����)
    public Player player { get; private set; }

    //private Animator animator; // ������ �ִϸ��̼� ��Ʈ�ѷ�
    private SpriteRenderer weaponRenderer; // ������ ��������Ʈ ������


    /// <summary>
    /// �ʱ�ȭ �۾��� �����ϴ� �޼���
    /// </summary>
    public virtual void Init()
    {
        // �θ� ��ü���� BaseController�� ������
        player = transform.parent.parent.GetComponent<Player>();

        // ���� ��ü���� Animator �� SpriteRenderer�� ������
        //animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        // ���� �ӵ��� ���� �ִϸ��̼� �ӵ� ����
        //animator.speed = 1.0f / delay;

        // ������ ũ�� ����
        transform.localScale = Vector3.one * weaponSize;
    }

    /// <summary>
    /// Start���� ������ �߰����� ���� (����� �����, �ʿ� �� �������̵� ����)
    /// </summary>
    protected virtual void Start()
    {
        Debug.Log($"�ʱ� ���ݷ�: {damage}, �ʱ� �ӵ�: {AttackSpeed}, �ʱ� ������: {Delay}");
        SkillManager skillManager = FindObjectOfType<SkillManager>();
        if(skillManager == null)
        {
            Debug.LogError("SkillManager�� ã�� �� �����ϴ�.");
            return;
        }

    }

    /// <summary>
    /// ������ �����ϴ� �޼��� (�ڽ� Ŭ�������� �������̵� ����)
    /// </summary>
    public virtual void Attack()
    {
        //AttackAnimation();

    }

    /// <summary>
    /// ���� �ִϸ��̼��� �����ϴ� �޼���
    /// </summary>
    //public void AttackAnimation()
    //{
    //    animator.SetTrigger(IsAttack);
    //}

    /// <summary>
    /// ������ ������ ȸ����Ű�� �޼���
    /// </summary>
    /// <param name="isLeft">true�� ����, false�� ������</param>
    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
