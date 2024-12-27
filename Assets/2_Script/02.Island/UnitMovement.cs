using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float moveSpeed = 4f; // 이동 속도
    public Transform targetEnemyBase; // 적 기지 Transform

    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private UnitAttack unitAttack; // UnitAttack 스크립트 참조
    private Animator animator; // Animator 컴포넌트

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        unitAttack = GetComponentInChildren<UnitAttack>(); // 하위 오브젝트의 UnitAttack 스크립트 가져오기
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기

        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트가 필요합니다.");
        }
    }

    void Update()
    {
        // 공격 중이면 이동 멈추고 애니메이션 비활성화
        if (unitAttack != null && unitAttack.isAttacking)
        {
            rb.velocity = Vector2.zero; // 이동 멈춤
            UpdateWalkingAnimation(false); // 이동 애니메이션 비활성화
        }
        else
        {
            // 적 기지 방향으로 이동
            if(targetEnemyBase != null)
            {
                MoveTowardsTarget(targetEnemyBase.position);
                UpdateWalkingAnimation(true); // 이동 애니메이션 활성화
            }
           
        }
    }

    void MoveTowardsTarget(Vector2 targetPosition)
    {
        // 목표 방향으로 이동
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    void UpdateWalkingAnimation(bool isWalking)
    {
        // 애니메이터의 IsWalking 파라미터 업데이트
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
        }
    }
}
