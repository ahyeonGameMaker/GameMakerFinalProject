using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float followDistance = 2f; // �÷��̾���� ������ �Ÿ�
    public float moveSpeed = 2f; // ���� �̵� �ӵ�

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FollowPlayer();
        bool isMoving = rb.velocity.sqrMagnitude > 0.01f;
        UpdateAnimationState(isMoving);
    }

    public void MoveToTarget(Vector2 targetPosition)
    {
        // ���� ���� ���
        Vector2 direction = (targetPosition - (Vector2)transform.position);

        if (direction.sqrMagnitude > 0.01f) // ��ǥ ��ġ�� ����� �Ÿ��� ���� �̵�
        {
            direction.Normalize(); // ������ ����ȭ
            rb.velocity = direction * moveSpeed;
            UpdateFlip(direction.x);
        }
        else
        {
            StopMovement(); // �ʹ� ������ ����
        }
    }



    public void FollowPlayer()
    {
        if (player == null) return;

        Vector2 targetPosition = player.position;
        float distanceToPlayer = Vector2.Distance(transform.position, targetPosition);

        if (distanceToPlayer > followDistance + 0.1f) // ��ǥ �Ÿ����� �ణ �ָ� ���� ��쿡�� �̵�
        {
            Debug.Log("��ǥ�� �̵� ��...");
            MoveToTarget(targetPosition);
        }
        else if (distanceToPlayer <= followDistance)
        {
            Debug.Log("��ǥ�� �����Ͽ� ����");
            StopMovement();
        }
    }


    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        UpdateFlip(0); // ���� ���¿����� ���� ������Ʈ
    }

    private void UpdateFlip(float moveDirection)
    {
        if (moveDirection != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (moveDirection > 0 ? -1 : 1);
            transform.localScale = scale;
        }
    }

    private void UpdateAnimationState(bool isMoving)
    {
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