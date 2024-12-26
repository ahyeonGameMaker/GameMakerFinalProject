using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 4f; // �̵� �ӵ�
    public Transform targetBase; // ��ǥ ���� Transform

    private Rigidbody2D rb; // Rigidbody2D ������Ʈ
    private EnemyAttack enemyAttack; // EnemyAttack ��ũ��Ʈ ����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponentInChildren<EnemyAttack>(); // ���� ������Ʈ�� EnemyAttack ��ũ��Ʈ ��������
    }

    void Update()
    {
        // ���� ���̸� �̵� �ߴ�
        if (enemyAttack != null && enemyAttack.isAttacking)
        {
            rb.velocity = Vector2.zero; // �̵� ����
        }
        else
        {
            // ���� �������� �̵�
            MoveTowardsTarget(targetBase.position);
        }
    }

    void MoveTowardsTarget(Vector2 targetPosition)
    {
        // ��ǥ �������� �̵�
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }
}