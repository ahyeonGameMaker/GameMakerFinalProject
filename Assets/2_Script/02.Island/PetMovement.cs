using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float followDistance = 2f; // 플레이어와의 고정된 거리
    public float moveSpeed = 2f; // 펫의 이동 속도

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            Debug.LogError("플레이어가 설정되지 않았습니다.");
        }
    }

    public void MoveToTarget(Vector2 targetPosition)
    {
        // 목표 위치로 이동
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        UpdateFlip(direction.x);
    }

    public void FollowPlayer()
    {
        // 플레이어 기준 위치 유지
        if (player == null) return;

        Vector2 targetPosition = (Vector2)player.position;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 플레이어와의 고정된 거리를 유지하지 못하면 이동
        if (distanceToPlayer > followDistance)
        {
            MoveToTarget(targetPosition);
        }
        else
        {
            StopMovement(); // 거리를 유지하면 정지
        }
    }

    public void StopMovement()
    {
        // 펫이 멈추도록 속도를 0으로 설정
        rb.velocity = Vector2.zero;
    }

    private void UpdateFlip(float moveDirection)
    {
        // Transform 스케일을 사용해 펫이 이동 방향에 따라 반전되도록 설정
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (moveDirection > 0 ? 1 : -1);
        transform.localScale = scale;
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
