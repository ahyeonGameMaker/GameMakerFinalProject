using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // 최대 체력
    public int currentHealth;
    private Animator animator; // Animator 컴포넌트
    private bool isDead = false; // 사망 상태 플래그

    void Start()
    {
        // 초기화
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기

        if (animator == null)
        {
            Debug.Log("Animator 컴포넌트가 필요합니다.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // 이미 사망 상태라면 처리하지 않음

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
        if (isDead) return; // 이미 사망 상태라면 처리하지 않음

        isDead = true; // 사망 상태 설정
        Debug.Log($"{gameObject.name} 사망");

        // 사망 애니메이션 트리거
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // 사망 애니메이션 재생 후 오브젝트 삭제
        StartCoroutine(DestroyAfterAnimation());
    }

    private System.Collections.IEnumerator DestroyAfterAnimation()
    {
        if (animator != null)
        {
            // 사망 애니메이션 길이만큼 대기
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        Destroy(gameObject); // 오브젝트 삭제
    }
}
