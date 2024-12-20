using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;

    public delegate void EnemyDeathHandler();
    public event EnemyDeathHandler OnEnemyDeath;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 적 사망 이벤트 호출
        OnEnemyDeath?.Invoke();
        Destroy(gameObject); // 적 제거
    }
}
