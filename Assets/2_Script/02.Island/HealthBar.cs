using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health targetHealth; // 체력 데이터를 관리하는 Health 스크립트
    public Image fillImage;     // Fill Amount를 사용할 이미지

    private void Start()
    {
        targetHealth = GetComponent<Health>();
    }

    void Update()
    {
        if (targetHealth != null && fillImage != null)
        {
            // Fill Amount를 현재 체력 비율에 맞게 업데이트
            fillImage.fillAmount = Mathf.Clamp01((float)targetHealth.GetCurrentHealth() / targetHealth.maxHealth);
        }
    }

    public void UpdateHealthBarImmediately()
    {
        if (targetHealth != null && fillImage != null)
        {
            fillImage.fillAmount = Mathf.Clamp01((float)targetHealth.GetCurrentHealth() / targetHealth.maxHealth);
        }
    }
}
