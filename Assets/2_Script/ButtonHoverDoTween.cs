using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverDOTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Scaling Settings")]
	private Vector3 originalScale;
	[SerializeField] private float scaleAmount = 1.1f;
	[SerializeField] private float duration = 0.2f;

	[Header("Image Settings")]
	[SerializeField] private Image buttonImage;  // ��ư�� Image ������Ʈ
	public Sprite OnSprite;  // ���콺 ���� �� �̹���
	public Sprite OffSprite;  // �⺻ �̹���
	private bool IsSceneButton = false;
	public Image clearImage;

	void Start()
	{
		originalScale = transform.localScale;

		// ��ư �̹��� �ڵ����� �Ҵ�
		if (buttonImage == null)
		{
			buttonImage = GetComponent<Image>();
			Debug.Log("buttonImage �ʱ�ȭ");
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		// ��ư Ȯ�� �ִϸ��̼�
		transform.DOScale(originalScale * scaleAmount, duration)
				 .SetEase(Ease.OutBack);

		// �̹��� ����
		if (buttonImage && OnSprite)
		{
			buttonImage.sprite = OnSprite;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		// ���� ũ��� ����
		transform.DOScale(originalScale, duration)
				 .SetEase(Ease.OutBack);

		// ���� �̹����� ����
		if (buttonImage && OffSprite && !IsSceneButton)
		{
			buttonImage.sprite = OffSprite;
		}
	}

	public void SetSceneButton(bool isScene)
	{
		IsSceneButton = isScene;
		if (isScene)
		{
			buttonImage.sprite = OnSprite;
		}
		else
		{
			buttonImage.sprite = OffSprite;
		}
	}
}

