using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundUI : MonoBehaviour
{
    public Transform background1;  // ù ��° ���
    public Transform background2;  // �� ��° ���
    public float scrollSpeed = 2f;  // ��� �̵� �ӵ�

    private Vector3 startPosition1;  // ù ��° ����� �ʱ� ��ġ
    private Vector3 startPosition2;  // �� ��° ����� �ʱ� ��ġ

    void Start()
    {
        // ����� �ʱ� ��ġ ����
        startPosition1 = background1.position;
        startPosition2 = background2.position;
    }

    void Update()
    {
        // ��� �̵�
        MoveBackground();
    }

    void MoveBackground()
    {
        // ��� �̵�
        background1.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        background2.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // ù ��° ����� ȭ�� ������ ������ ��, �� ��° ��� �ڷ� ������
        if (background1.position.x <= -background1.GetComponent<SpriteRenderer>().bounds.size.x)
        {
            background1.position = new Vector3(background2.position.x + background2.GetComponent<SpriteRenderer>().bounds.size.x, background1.position.y, background1.position.z);
        }

        // �� ��° ����� ȭ�� ������ ������ ��, ù ��° ��� �ڷ� ������
        if (background2.position.x <= -background2.GetComponent<SpriteRenderer>().bounds.size.x)
        {
            background2.position = new Vector3(background1.position.x + background1.GetComponent<SpriteRenderer>().bounds.size.x, background2.position.y, background2.position.z);
        }
    }
}
