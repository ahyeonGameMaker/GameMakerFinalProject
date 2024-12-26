using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10; // ���ݷ�
    public float attackCooldown = 1f; // ���� ����
    public LayerMask enemyLayer; // ���� ��� ���̾�

    private float attackTimer; // ���� ���� ��Ÿ�� Ÿ�̸�
    private Collider2D currentTarget; // ���� ���� ���
    private Animator animator; // �ִϸ����� ������Ʈ

    void Start()
    {
        attackTimer = 0f; // �ʱ� Ÿ�̸� ����
        animator = GetComponentInParent<Animator>(); // Animator ������Ʈ ��������
    }

    void Update()
    {
        // ��Ÿ�� ����
        attackTimer -= Time.deltaTime;

        // ���� ����� ���� ��� �ڵ� ����
        if (currentTarget != null && attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown; // ��Ÿ�� �ʱ�ȭ
        }
    }

    private void Attack()
    {
        Debug.Log("�÷��̾ �����߽��ϴ�!");

        // �ִϸ������� Attack Ʈ���� Ȱ��ȭ
        if (animator != null)
        {
            animator.SetTrigger("IsAttack");
        }

        // ���� Ÿ�ٿ� �������� ����
        if (currentTarget != null)
        {
            // Enemy ������Ʈ�� ������ ����
            var enemyHealth = currentTarget.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                return; // Enemy�� �������� ���������� ����
            }

            // Health ������Ʈ�� ������ ����
            var targetHealth = currentTarget.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage);
                return; // Health�� �������� ���������� ����
            }

            Debug.LogWarning($"{currentTarget.name}�� �������� ���� �� �ִ� ������Ʈ�� �����ϴ�.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �� ���̾ ���� ������Ʈ�� ���� ������� ����
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            currentTarget = collision; // Ÿ�� ����
            Debug.Log($"�� ����: {collision.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ���� ������ ����� Ÿ�� ����
        if (collision == currentTarget)
        {
            currentTarget = null;
            Debug.Log($"�� ���� ��Ż: {collision.name}");
        }
    }
}
