using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float followDistance = 2f; // �÷��̾���� ������ �Ÿ�
    public float moveSpeed = 2f; // ���� �̵� �ӵ�

    private Rigidbody2D rb;
    private Animator animator; // Animator ������Ʈ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (player == null)
        {
            Debug.LogError("�÷��̾ �������� �ʾҽ��ϴ�.");
        }

        if (animator == null)
        {
            Debug.LogError("Animator ������Ʈ�� �ʿ��մϴ�.");
        }
    }

    public void MoveToTarget(Vector2 targetPosition)
    {
        // ��ǥ ��ġ�� �̵�
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        UpdateFlip(direction.x);
        UpdateAnimationState(true); // �̵� �ִϸ��̼� Ȱ��ȭ
    }

    public void FollowPlayer()
    {
        // �÷��̾� ���� ��ġ ����
        if (player == null) return;

        Vector2 targetPosition = (Vector2)player.position;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // �÷��̾���� ������ �Ÿ��� �������� ���ϸ� �̵�
        if (distanceToPlayer > followDistance)
        {
            MoveToTarget(targetPosition);
        }
        else
        {
            StopMovement(); // �Ÿ��� �����ϸ� ����
        }
    }

    public void StopMovement()
    {
        // ���� ���ߵ��� �ӵ��� 0���� ����
        rb.velocity = Vector2.zero;
        UpdateAnimationState(false); // �̵� �ִϸ��̼� ��Ȱ��ȭ
    }

    private void UpdateFlip(float moveDirection)
    {
        // Transform �������� ����� ���� �̵� ���⿡ ���� �����ǵ��� ����
        if (moveDirection != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (moveDirection > 0 ? -1 : 1);
            transform.localScale = scale;
        }
    }

    private void UpdateAnimationState(bool isMoving)
    {
        // �ִϸ������� isMoving �Ķ���� ������Ʈ
        if (animator != null)
        {
            animator.SetBool("IsWalking", isMoving);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(player.position, followDistance);
        }
    }
}
