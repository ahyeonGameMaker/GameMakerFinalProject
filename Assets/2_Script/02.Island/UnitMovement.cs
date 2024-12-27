using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float moveSpeed = 4f; // �̵� �ӵ�
    public Transform targetEnemyBase; // �� ���� Transform

    private Rigidbody2D rb; // Rigidbody2D ������Ʈ
    private UnitAttack unitAttack; // UnitAttack ��ũ��Ʈ ����
    private Animator animator; // Animator ������Ʈ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        unitAttack = GetComponentInChildren<UnitAttack>(); // ���� ������Ʈ�� UnitAttack ��ũ��Ʈ ��������
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������

        if (animator == null)
        {
            Debug.LogError("Animator ������Ʈ�� �ʿ��մϴ�.");
        }
    }

    void Update()
    {
        // ���� ���̸� �̵� ���߰� �ִϸ��̼� ��Ȱ��ȭ
        if (unitAttack != null && unitAttack.isAttacking)
        {
            rb.velocity = Vector2.zero; // �̵� ����
            UpdateWalkingAnimation(false); // �̵� �ִϸ��̼� ��Ȱ��ȭ
        }
        else
        {
            // �� ���� �������� �̵�
            if(targetEnemyBase != null)
            {
                MoveTowardsTarget(targetEnemyBase.position);
                UpdateWalkingAnimation(true); // �̵� �ִϸ��̼� Ȱ��ȭ
            }
           
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
        // �ִϸ������� IsWalking �Ķ���� ������Ʈ
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
        }
    }
}
