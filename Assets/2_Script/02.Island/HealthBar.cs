using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health targetHealth; // ü�� �����͸� �����ϴ� Health ��ũ��Ʈ
    public Image fillImage;     // Fill Amount�� ����� �̹���

    private void Start()
    {
        targetHealth = GetComponent<Health>();
    }

    void Update()
    {
        if (targetHealth != null && fillImage != null)
        {
            // Fill Amount�� ���� ü�� ������ �°� ������Ʈ
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
