using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float followDistance = 2f; // 플레이어와의 고정된 거리
    public float moveSpeed = 2f; // 펫의 이동 속도

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
        // 방향 벡터 계산
        Vector2 direction = (targetPosition - (Vector2)transform.position);

        if (direction.sqrMagnitude > 0.01f) // 목표 위치와 충분한 거리일 때만 이동
        {
            direction.Normalize(); // 방향을 정규화
            rb.velocity = direction * moveSpeed;
            UpdateFlip(direction.x);
        }
        else
        {
            StopMovement(); // 너무 가까우면 멈춤
        }
    }



    public void FollowPlayer()
    {
        if (player == null) return;

        Vector2 targetPosition = player.position;
        float distanceToPlayer = Vector2.Distance(transform.position, targetPosition);

        if (distanceToPlayer > followDistance + 0.1f) // 목표 거리보다 약간 멀리 있을 경우에만 이동
        {
            Debug.Log("목표로 이동 중...");
            MoveToTarget(targetPosition);
        }
        else if (distanceToPlayer <= followDistance)
        {
            Debug.Log("목표에 도달하여 정지");
            StopMovement();
        }
    }


    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        UpdateFlip(0); // 정지 상태에서도 방향 업데이트
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