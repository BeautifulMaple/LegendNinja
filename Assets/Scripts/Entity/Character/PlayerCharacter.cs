using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	[SerializeField] private SpriteRenderer characterSpriteRenderer; // �÷��̾� ��������Ʈ ������
	[SerializeField] private Animator animator;
	public void SetCharacter(CharacterData newCharacter)
	{
		if (newCharacter == null)
		{
			Debug.LogWarning("������ ĳ���� �����Ͱ� �ùٸ��� �ʽ��ϴ�.");
			return;
		}

		if (characterSpriteRenderer == null)
		{
			Debug.LogError("PlayerCharacter: SpriteRenderer�� �������� �ʾҽ��ϴ�!");
			return;
		}

		// �ִϸ��̼� ��Ȱ�� �� ��������Ʈ ����
		if (animator != null)
		{
			animator.enabled = false;
		}

		characterSpriteRenderer.sprite = newCharacter.characterSprite;
		Debug.Log($" ����� ��������Ʈ : {characterSpriteRenderer.sprite.name}");

		if (animator != null && newCharacter.characterAnimator != null)
		{
			animator.runtimeAnimatorController = newCharacter.characterAnimator;
			Debug.Log($" �ִϸ����� ����: {newCharacter.characterAnimator.name}");

		}
		else
		{
			Debug.LogWarning(" AnimatorOverrideController�� �������� �ʾҽ��ϴ�.");
		}

		// ��� �� �ִϸ����� �ٽ� Ȱ��ȭ(�ִϸ��̼� ����)
		StartCoroutine(ReenableAnimator());
	}

	private System.Collections.IEnumerator ReenableAnimator()
	{
		yield return new WaitForSeconds(0.1f);
		if (animator != null)
		{
			animator.enabled = true;
		}
	}

}
