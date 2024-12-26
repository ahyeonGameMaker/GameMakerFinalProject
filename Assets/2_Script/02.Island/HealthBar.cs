using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health targetHealth; // 체력 데이터를 관리하는 Health 스크립트
    public Image fillImage;     // Fill Amount를 사용할 이미지
    public float smoothSpeed = 5f; // 부드럽게 변화하는 속도

    private float targetFillAmount; // 목표 fillAmount

    private void Start()
    {
        targetHealth = GetComponent<Health>();
        if (targetHealth != null)
        {
            targetFillAmount = Mathf.Clamp01((float)targetHealth.GetCurrentHealth() / targetHealth.maxHealth);
            if (fillImage != null)
            {
                fillImage.fillAmount = targetFillAmount;
            }
        }
    }

    void Update()
    {
        if (targetHealth != null && fillImage != null)
        {
            // 목표 fillAmount 계산
            targetFillAmount = Mathf.Clamp01((float)targetHealth.GetCurrentHealth() / targetHealth.maxHealth);

            // 현재 fillAmount를 목표로 부드럽게 변화시킴
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFillAmount, Time.deltaTime * smoothSpeed);
        }
    }

    public void UpdateHealthBarImmediately()
    {
        if (targetHealth != null && fillImage != null)
        {
            // 목표 값과 현재 값을 즉시 동기화
            targetFillAmount = Mathf.Clamp01((float)targetHealth.GetCurrentHealth() / targetHealth.maxHealth);
            fillImage.fillAmount = targetFillAmount;
        }
    }
}
