using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_ : MonoBehaviour
{
    
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

}
