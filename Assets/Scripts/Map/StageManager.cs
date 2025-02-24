using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    private bool isStageCleared = false; //Ŭ���� �Ǵ�
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Ŭ���� �� �������� (��: ��� ���� óġ���� ��)
    public void ClearStage()
    {
        isStageCleared = true;
        Debug.Log("�������� Ŭ����! �ⱸ�� �̵��ϼ���.");
    }

    private void OnTriggerEnter2D(Collider2D other) //Ŭ���� �� �ݶ��̴��� ���� ���� �۵�
    {
        if (other.CompareTag("Player") && isStageCleared) 
        {
            LoadNextStage();
        }
    }

    void LoadNextStage()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); // ������
    }

    //if �� ���� óġ�ÿ� StageManager.instance.ClearStage(); ȣ��
}
