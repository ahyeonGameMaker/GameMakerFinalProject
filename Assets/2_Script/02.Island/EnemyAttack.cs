using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10; // ���ݷ�
    public float attackCooldown = 1f; // ���� ��Ÿ��

    private float attackTimer; // ���� ���� ��Ÿ�� Ÿ�̸�
    private Transform currentTarget; // ���� ������ ���� ���
    [HideInInspector] public bool isAttacking; // �̵��� ���߱� ���� ���� �÷���

    void Start()
    {
        attackTimer = 0f; // Ÿ�̸� �ʱ�ȭ
        isAttacking = false; // �ʱ� ����: ���� �� �ƴ�
    }

    void Update()
    {
        // ��Ÿ�� Ÿ�̸� ������Ʈ
        attackTimer -= Time.deltaTime;

        // ���� ����� �����ϸ� ����
        if (currentTarget != null && attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown; // ��Ÿ�� �ʱ�ȭ
        }
    }

    private void Attack()
    {
        // ����� ��ȿ���� Ȯ��
        if (currentTarget != null)
        {
            Debug.Log($"���� {currentTarget.name}�� ����!");
            var targetHealth = currentTarget.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage); // ü�� ���� ȣ��
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // FriendlyUnit �±׸� ���� ������Ʈ�� Ÿ������ ����
        if (collision.CompareTag("FriendlyUnit"))
        {
            currentTarget = collision.transform; // ���� ��� ����
            isAttacking = true; // �̵� �ߴ�
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ���� ����� ������ ����� ��� ����
        if (collision.transform == currentTarget)
        {
            currentTarget = null;
            isAttacking = false; // �̵� �簳
        }
    }
}
