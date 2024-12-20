using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // 최대 체력
    private int currentHealth;

    void Start()
    {
        // 초기화
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // 데미지를 받고 체력 감소
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} 체력: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        // 현재 체력 반환
        return currentHealth;
    }

    void Die()
    {
        // 유닛이 사망했을 때 처리
        Debug.Log($"{gameObject.name} 사망");
        Destroy(gameObject);
    }
}
