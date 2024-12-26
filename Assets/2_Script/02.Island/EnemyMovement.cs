using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 4f; // �̵� �ӵ�
    public Transform targetBase; // ��ǥ ���� Transform

    private Rigidbody2D rb; // Rigidbody2D ������Ʈ
    private EnemyAttack enemyAttack; // EnemyAttack ��ũ��Ʈ ����
    private Animator animator; // Animator ������Ʈ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponentInChildren<EnemyAttack>(); // ���� ������Ʈ�� EnemyAttack ��ũ��Ʈ ��������
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������

        if (animator == null)
        {
            Debug.LogError("Animator ������Ʈ�� �ʿ��մϴ�.");
        }
    }

    void Update()
    {
        // ���� ���̸� �̵� ���߰� �ִϸ��̼� ��Ȱ��ȭ
        if (enemyAttack != null && enemyAttack.isAttacking)
        {
            rb.velocity = Vector2.zero; // �̵� ����
            UpdateWalkingAnimation(false); // �̵� �ִϸ��̼� ��Ȱ��ȭ
        }
        else
        {
            // ���� �������� �̵�
            MoveTowardsTarget(targetBase.position);
            UpdateWalkingAnimation(true); // �̵� �ִϸ��̼� Ȱ��ȭ
        }
    }

    void MoveTowardsTarget(Vector2 targetPosition)
    {
        // ��ǥ �������� �̵�
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    void UpdateWalkingAnimation(bool isWalking)
    {
        // �ִϸ������� isWalking �Ķ���� ������Ʈ
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
        }
    }
}
