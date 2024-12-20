using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // �ִ� ü��
    private int currentHealth;

    void Start()
    {
        // �ʱ�ȭ
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // �������� �ް� ü�� ����
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} ü��: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        // ���� ü�� ��ȯ
        return currentHealth;
    }

    void Die()
    {
        // ������ ������� �� ó��
        Debug.Log($"{gameObject.name} ���");
        Destroy(gameObject);
    }
}
