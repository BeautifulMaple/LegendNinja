using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private Image characterImage; // ĳ���� ������
    [SerializeField] private TextMeshProUGUI characterNameText; // ĳ���� �̸�

    private CharacterData characterData;
    private CharacterSelector characterSelector;

    public void Setup(CharacterData character, CharacterSelector selector)
    {
        characterData = character;
        characterSelector = selector;

        characterImage.sprite = character.characterSprite;
        characterNameText.text = character.characterName;

        GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log($" {characterData.characterName} ��ư Ŭ����!");
            characterSelector.SelectCharacter(characterData);
        });
    }
}
