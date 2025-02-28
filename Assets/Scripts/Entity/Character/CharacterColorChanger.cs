using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterColorChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterSprite;  // 2D ĳ���� ��������Ʈ
    [SerializeField] private Image characterPreviewImage; // UI ĳ���� �̸�����
    [SerializeField] private Image previewImage; // �̸����� ���� UI

    [SerializeField] private Slider rSlider, gSlider, bSlider, aSlider;
    [SerializeField] private TMP_InputField rInput, gInput, bInput, aInput;

    private Color currentColor;

    void Start()
    {
        characterPreviewImage.sprite = characterSprite.sprite;

        //  �ʱ� ������ characterSprite�� material.color���� ������
        currentColor = characterSprite.material.color;

        //  �����̴� �� ����
        rSlider.value = currentColor.r;
        gSlider.value = currentColor.g;
        bSlider.value = currentColor.b;
        aSlider.value = currentColor.a;

        //  �Է� �ʵ� �� ���� (0~1 ������ 255 �������� ��ȯ)
        rInput.text = Mathf.RoundToInt(currentColor.r * 255).ToString();
        gInput.text = Mathf.RoundToInt(currentColor.g * 255).ToString();
        bInput.text = Mathf.RoundToInt(currentColor.b * 255).ToString();
        aInput.text = Mathf.RoundToInt(currentColor.a * 255).ToString();

        // �����̴� �� ���� �� �̺�Ʈ ����
        rSlider.onValueChanged.AddListener(UpdateColorFromSlider);
        gSlider.onValueChanged.AddListener(UpdateColorFromSlider);
        bSlider.onValueChanged.AddListener(UpdateColorFromSlider);
        aSlider.onValueChanged.AddListener(UpdateColorFromSlider);

        // �Է� �ʵ� �� ���� �� �̺�Ʈ ����
        rInput.onEndEdit.AddListener(UpdateColorFromInput);
        gInput.onEndEdit.AddListener(UpdateColorFromInput);
        bInput.onEndEdit.AddListener(UpdateColorFromInput);
        aInput.onEndEdit.AddListener(UpdateColorFromInput);

        ApplyColorToUI();
    }

    //  �����̴� �� ���� �� ���� ������Ʈ
    private void UpdateColorFromSlider(float value)
    {
        currentColor = new Color(rSlider.value, gSlider.value, bSlider.value, aSlider.value);
        ApplyColor();
    }

    //  �Է� �ʵ� �� ���� �� ���� ������Ʈ
    private void UpdateColorFromInput(string value)
    {
        float r = Mathf.Clamp01(float.Parse(rInput.text) / 255f);
        float g = Mathf.Clamp01(float.Parse(gInput.text) / 255f);
        float b = Mathf.Clamp01(float.Parse(bInput.text) / 255f);
        float a = Mathf.Clamp01(float.Parse(aInput.text) / 255f);

        rSlider.value = r;
        gSlider.value = g;
        bSlider.value = b;
        aSlider.value = a;

        currentColor = new Color(r, g, b, a);
        ApplyColor();
    }

    //  UI ��� ������Ʈ (�̸����� & ���� �Է� �ʵ� �ݿ�)
    private void ApplyColorToUI()
    {
        rInput.text = Mathf.RoundToInt(currentColor.r * 255).ToString();
        gInput.text = Mathf.RoundToInt(currentColor.g * 255).ToString();
        bInput.text = Mathf.RoundToInt(currentColor.b * 255).ToString();
        aInput.text = Mathf.RoundToInt(currentColor.a * 255).ToString();

        previewImage.color = currentColor;
        characterPreviewImage.color = currentColor;
    }

    //  ĳ���� ���� ����
    private void ApplyColor()
    {
        characterSprite.material.color = currentColor; //  material.color ���
        previewImage.color = currentColor;
        characterPreviewImage.color = currentColor;
    }
}
