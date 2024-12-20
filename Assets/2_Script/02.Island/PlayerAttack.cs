using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10; // ���ݷ�
    public float attackCooldown = 1f; // ���� ����
    public LayerMask enemyLayer; // ���� ��� ���̾�

    private float attackTimer; // ���� ���� ��Ÿ�� Ÿ�̸�
    private Collider2D currentTarget; // ���� ���� ���

    void Start()
    {
        attackTimer = 0f; // �ʱ� Ÿ�̸� ����
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

        // ���� Ÿ�ٿ� �������� ����
        var enemyHealth = currentTarget.GetComponent<Enemy>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage);
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
