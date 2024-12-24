using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float followDistance = 2f; // �÷��̾���� ������ �Ÿ�
    public float moveSpeed = 2f; // ���� �̵� �ӵ�

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            Debug.LogError("�÷��̾ �������� �ʾҽ��ϴ�.");
        }
    }

    public void MoveToTarget(Vector2 targetPosition)
    {
        // ��ǥ ��ġ�� �̵�
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        UpdateFlip(direction.x);
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
    }

    private void UpdateFlip(float moveDirection)
    {
        // Transform �������� ����� ���� �̵� ���⿡ ���� �����ǵ��� ����
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
