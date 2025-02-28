using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationUIManager : MonoBehaviour
{
    [SerializeField] private GameObject colorPanel; // ���� ����
    [SerializeField] private GameObject exteriorPanel;  // ���� ����
    [SerializeField] private Button colorButton; // ���� ���� ��ư
    [SerializeField] private Button exteriorButton;  // ���� ���� ��ư
    // Start is called before the first frame update
    void Start()
    {
        colorButton.onClick.AddListener(ShowColorPenel);
        exteriorButton.onClick.AddListener(ShowExeriorPenel);
    }

    private void ShowColorPenel()
    {
        colorPanel.SetActive(true);  // ���� �г� Ȱ��ȭ
        exteriorPanel.SetActive(false);  // ���� �г� ��Ȱ��ȭ
    }


    private void ShowExeriorPenel()
    {
        colorPanel.SetActive(false);  // ���� �г� ��Ȱ��ȭ
        exteriorPanel.SetActive(true);  // ���� �г� Ȱ��ȭ
    }

}
