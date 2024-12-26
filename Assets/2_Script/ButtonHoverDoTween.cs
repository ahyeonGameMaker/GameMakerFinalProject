using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

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

	void Start()
	{
		originalScale = transform.localScale;

		// ��ư �̹��� �ڵ����� �Ҵ�
		if (buttonImage == null)
			buttonImage = GetComponent<Image>();
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
		if (buttonImage && OffSprite)
		{
			buttonImage.sprite = OffSprite;
		}
	}
}
