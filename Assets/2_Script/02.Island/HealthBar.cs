using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health targetHealth; // ü�� �����͸� �����ϴ� Health ��ũ��Ʈ
    public Image fillImage;     // Fill Amount�� ����� �̹���
    public float smoothSpeed = 5f; // �ε巴�� ��ȭ�ϴ� �ӵ�

    private float targetFillAmount; // ��ǥ fillAmount

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
            // ��ǥ fillAmount ���
            targetFillAmount = Mathf.Clamp01((float)targetHealth.GetCurrentHealth() / targetHealth.maxHealth);

            // ���� fillAmount�� ��ǥ�� �ε巴�� ��ȭ��Ŵ
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFillAmount, Time.deltaTime * smoothSpeed);
        }
    }

    public void UpdateHealthBarImmediately()
    {
        if (targetHealth != null && fillImage != null)
        {
            // ��ǥ ���� ���� ���� ��� ����ȭ
            targetFillAmount = Mathf.Clamp01((float)targetHealth.GetCurrentHealth() / targetHealth.maxHealth);
            fillImage.fillAmount = targetFillAmount;
        }
    }
}
