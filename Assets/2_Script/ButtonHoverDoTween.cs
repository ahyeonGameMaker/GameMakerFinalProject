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
	[SerializeField] private Image buttonImage;  // 버튼의 Image 컴포넌트
	public Sprite OnSprite;  // 마우스 오버 시 이미지
	public Sprite OffSprite;  // 기본 이미지
	private bool IsSceneButton = false;
	public Image clearImage;

	void Start()
	{
		originalScale = transform.localScale;

		// 버튼 이미지 자동으로 할당
		if (buttonImage == null)
		{
			buttonImage = GetComponent<Image>();
			Debug.Log("buttonImage 초기화");
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		// 버튼 확대 애니메이션
		transform.DOScale(originalScale * scaleAmount, duration)
				 .SetEase(Ease.OutBack);

		// 이미지 변경
		if (buttonImage && OnSprite)
		{
			buttonImage.sprite = OnSprite;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		// 원래 크기로 복귀
		transform.DOScale(originalScale, duration)
				 .SetEase(Ease.OutBack);

		// 원래 이미지로 복귀
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

