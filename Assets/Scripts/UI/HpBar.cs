using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Transform player;  // �÷��̾� ������Ʈ
    public Image progress;    // ü�¹� �̹���

    public Vector3 offset = new Vector3(0f, 1.5f, 0f); // ü�¹� ������ (�÷��̾� ��ġ���� ���� ���� ����)

    private Player playerScript; // Player ��ũ��Ʈ ����

    private void Start()
    {
        playerScript = player.GetComponent<Player>(); // �÷��̾� ��ũ��Ʈ ��������
    }

    private void Update()
    {
        // �÷��̾ ������ ü�¹� ��ġ ������Ʈ
        if (player != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(player.position + offset);

            // �÷��̾��� ü�¿� ���� fillAmount ������Ʈ
            if (playerScript != null)
            {
                float fillAmount = Mathf.Clamp(playerScript.Health / playerScript.MaxHealth, 0f, 1f);
                progress.fillAmount = fillAmount; // ü�¿� ���� ä����
            }
        }
    }
}
