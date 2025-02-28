using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    [Header(" ĳ���� ������")]
    [SerializeField] private CharacterList characterList; // ĳ���� ����Ʈ (ScriptableObject)

    [Header(" UI ���")]
    [SerializeField] private Transform content; // ScrollView�� Content
    [SerializeField] private GameObject characterButtonPrefab; // ĳ���� ��ư ������
    [SerializeField] private Image characterPreviewImage; // ������ ĳ���� �̸�����
    [SerializeField] private TextMeshProUGUI characterNameText; // ĳ���� �̸� ǥ��
    [SerializeField] private Button confirmButton; // ������ ĳ���� ���� ��ư

    [Header(" �÷��̾� ĳ����")]
    [SerializeField] private PlayerCharacter playerCharacter; // �÷��̾� ĳ����

    public static CharacterSelector Instance;
    public CharacterData SelectedCharacterData;
    private CharacterData selectedCharacter; // ���� ������ ĳ����

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� ����
        }
        else
        {
            Destroy(gameObject);
        }
        if (playerCharacter == null)
        {
            playerCharacter = FindObjectOfType<PlayerCharacter>();
            if (playerCharacter == null)
            {
                Debug.LogError(" ������ PlayerCharacter�� ã�� �� �����ϴ�!");
            }
        }
    }
    private void Start()
    {
        GenerateCharacterButtons();
        confirmButton.interactable = false; // ĳ���͸� ������ ������ ��Ȱ��ȭ
        confirmButton.onClick.AddListener(ApplySelectedCharacter);
    }

    private void GenerateCharacterButtons()
    {
        foreach (CharacterData character in characterList.characters)
        {
            GameObject newButton = Instantiate(characterButtonPrefab, content);
            CharacterButton buttonScript = newButton.GetComponent<CharacterButton>();
            buttonScript.Setup(character, this);
        }
    }

    public void SelectCharacter(CharacterData character)
    {
        if (character != null)
        {
            selectedCharacter = character;
            characterPreviewImage.sprite = selectedCharacter.characterSprite;
            characterNameText.text = selectedCharacter.characterName;

            Debug.Log($" �̸����� ������Ʈ: {selectedCharacter.characterName}");
            confirmButton.interactable = true; // ĳ���� ���� �� ��ư Ȱ��ȭ
        }
    }

    public void ApplySelectedCharacter()
    {
        if (selectedCharacter == null)
        {
            Debug.LogError("���õ� ĳ���Ͱ� �����ϴ�!");
            return;
        }

        Debug.Log($"{selectedCharacter.characterName} ĳ���� ���� �õ�!");

        if (playerCharacter == null)
        {
            Debug.LogError(" PlayerCharacter�� �������� �ʾҽ��ϴ�!");
            return;
        }

        playerCharacter.SetCharacter(selectedCharacter);
    }

}
