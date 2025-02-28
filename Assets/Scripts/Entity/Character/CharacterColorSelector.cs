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
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
