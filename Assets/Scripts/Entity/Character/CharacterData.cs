using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/New Character")]

public class CharacterData : ScriptableObject
{
    public string characterName; // ĳ���� �̸�
    public Sprite characterSprite; // ĳ���� ��������Ʈ
    public AnimatorOverrideController characterAnimator; //�ִϸ��̼� ��Ʈ�ѷ�
}

