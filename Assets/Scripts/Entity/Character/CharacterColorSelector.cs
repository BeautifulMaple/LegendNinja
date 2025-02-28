using UnityEngine;

public class CharacterColorSelector : MonoBehaviour
{
    public static CharacterColorSelector Instance;
    public Sprite SelectedCharacterSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
