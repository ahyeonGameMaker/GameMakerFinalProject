using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // �ִ� ü��
    public int currentHealth;
    private Animator animator; // Animator ������Ʈ
    private bool isDead = false; // ��� ���� �÷���

    void Start()
    {
        // �ʱ�ȭ
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������

        if (animator == null)
        {
            Debug.Log("Animator ������Ʈ�� �ʿ��մϴ�.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // �̹� ��� ���¶�� ó������ ����

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
        if (isDead) return; // �̹� ��� ���¶�� ó������ ����

        isDead = true; // ��� ���� ����
        Debug.Log($"{gameObject.name} ���");

        // ��� �ִϸ��̼� Ʈ����
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // ��� �ִϸ��̼� ��� �� ������Ʈ ����
        StartCoroutine(DestroyAfterAnimation());
    }

    private System.Collections.IEnumerator DestroyAfterAnimation()
    {
        if (animator != null)
        {
            // ��� �ִϸ��̼� ���̸�ŭ ���
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        Destroy(gameObject); // ������Ʈ ����
    }
}
