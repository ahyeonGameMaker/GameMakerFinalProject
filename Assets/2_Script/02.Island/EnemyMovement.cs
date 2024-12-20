using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 4f; // 이동 속도
    public Transform targetBase; // 목표 기지 Transform

    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private EnemyAttack enemyAttack; // EnemyAttack 스크립트 참조

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponentInChildren<EnemyAttack>(); // 하위 오브젝트의 EnemyAttack 스크립트 가져오기
    }

    void Update()
    {
        // 공격 중이면 이동 중단
        if (enemyAttack != null && enemyAttack.isAttacking)
        {
            rb.velocity = Vector2.zero; // 이동 멈춤
        }
        else
        {
            // 기지 방향으로 이동
            MoveTowardsTarget(targetBase.position);
        }
    }

    void MoveTowardsTarget(Vector2 targetPosition)
    {
        // 목표 방향으로 이동
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }
}
