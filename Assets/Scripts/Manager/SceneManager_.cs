using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneManager_ : MonoBehaviour
{
    public Button StartButton;
    public Button SettingButton;
    public Button ExitButton;
    public Button CloseButton;

    public GameObject settingUI;

    private void Start()
    {
        if (StartButton != null)
            StartButton.onClick.AddListener(OnButtonClick);

        if (SettingButton != null)
            SettingButton.onClick.AddListener(OnButtonClick);

        if (ExitButton != null)
            ExitButton.onClick.AddListener(OnButtonClick);

        if (CloseButton != null)
            CloseButton.onClick.AddListener(OnButtonClick);
    }



    private void OnButtonClick()
    {
        SoundManager.instance.PlaySFX(1);
    }
    public void StartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitScene()
    {
        // ���� ����
#if UNITY_EDITOR
        // �����Ϳ��� ���� ���� ���
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���忡�� ���� ���� ���
        Application.Quit();
#endif
    }

    public void OffSettingUI()
    {
        settingUI.SetActive(false);
    }
    public void OnSettingUI()
    {
        settingUI.SetActive(true);
    }
}
