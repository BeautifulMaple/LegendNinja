using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    private Animator animator;
    private Rigidbody2D rb;

    public void Move()
    {
        
        // WASD
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        Vector2 MoveDirection = new Vector2(MoveX, MoveY).normalized;

        // �ִϸ��̼� ó��
        if (MoveDirection.magnitude > 0)  // �̵� ���� ��
        {
            animator.SetBool("IsMove", true);
        }
        else
        {
            animator.SetBool("IsMove", false);
        }
        animator.SetFloat("MoveX", MoveDirection.x);
        animator.SetFloat("MoveY", MoveDirection.y);

        rb.velocity = MoveDirection * MoveSpeed;
    }

    public override void Attack()
    {

        // base.Attack();
    }

    public void UseSkill()
    {

    }
    public override void Damage(float damage)
    {
        Health -= damage; 
        
    }
    Transform FindCloseMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // "Monster" �±� ���� ������Ʈ ã��
        Transform ClosestEnemy = null; //���� ����� ���� ������ ����

        float MaxDistance = 50f; // ȭ�� �󿡼��� �ִ�Ÿ� ����
        float ClosestDistance = MaxDistance; // �ִ�Ÿ� ������ ������ �ʱ�ȭ
        
        Vector2 PlayerPos = transform.position; // �÷��̾� ��ġ

        foreach (GameObject monster in monsters)
        {
            float Distance = Vector2.Distance(PlayerPos, monster.transform.position); // �÷��̾�� ���� �Ÿ� ���
            if (Distance < ClosestDistance) //���ݱ��� ����� ���� ����� �Ÿ����� ������
            {
                ClosestDistance = Distance; // ���ο� ���� ����� �Ÿ��� ���� 
                ClosestEnemy = monster.transform; // �ش� ���� transform ����
            }
        }

        return ClosestEnemy; // ���� ����� �� ��ȯ (������ null)
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // ���Ƿ� �� ���� �߽��ϴ�.
        MaxHealth = 100f;
        Health = 100f;
        AttackPower = 10f;
        MoveSpeed = 3f;
    }

    void Update()
    {
        Move();
        
        Transform target = FindCloseMonster();
        if (target != null)
        {
            Debug.Log("���� ����� ��: " + target.name); // �� �̸� ���
        }
    }
}
