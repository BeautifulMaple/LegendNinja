using PublicDefinitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : BaseMonster
{
    
    private void Awake()
    {
        monsterAnimator = GetComponentInChildren<Animator>();
        GetComponentInChildren<CircleCollider2D>().radius = 3f;
    }
    public override void Attack()
    {
        base.Attack();

        if (AttackCoolDown > 0f) return;
        
        TargetPlayer.Damage(AttackPower);
        Debug.Log("�ٰŸ� ����!");
        AttackCoolDown = AttackTime;
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
