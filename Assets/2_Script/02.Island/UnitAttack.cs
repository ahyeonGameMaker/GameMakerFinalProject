using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    public int attackDamage = 15; // 공격력
    public float attackCooldown = 1.5f; // 공격 쿨타임

    private float attackTimer; // 현재 공격 쿨타임 타이머
    private Transform currentTarget; // 현재 감지된 공격 대상
    [HideInInspector] public bool isAttacking; // 이동을 멈추기 위한 상태 플래그

    private Animator animator; // Animator 컴포넌트

    void Start()
    {
        attackTimer = 0f; // 타이머 초기화
        isAttacking = false; // 초기 상태: 공격 중 아님
        animator = GetComponentInParent<Animator>(); // Animator 컴포넌트 가져오기

        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트가 필요합니다.");
        }
    }

    void Update()
    {
        // 쿨타임 타이머 업데이트
        attackTimer -= Time.deltaTime;

        // 공격 대상이 존재하면 공격
        if (currentTarget != null && attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown; // 쿨타임 초기화
        }
    }

    private void Attack()
    {
        // 애니메이션 트리거 실행
        if (animator != null)
        {
            animator.SetTrigger("IsAttack");
        }

        // 대상이 유효한지 확인
        if (currentTarget != null)
        {
            Debug.Log($"유닛이 {currentTarget.name}을 공격!");

            // 대상이 Enemy일 경우
            var enemyComponent = currentTarget.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
                return;
            }

            // 대상이 Health일 경우
            var healthComponent = currentTarget.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(attackDamage);
                return;
            }

            Debug.LogWarning($"{currentTarget.name}는 데미지를 받을 수 있는 컴포넌트가 없습니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy 태그 또는 Health 컴포넌트를 가진 오브젝트만 타겟으로 설정
        if (collision.CompareTag("Enemy") || collision.GetComponent<Health>() != null)
        {
            currentTarget = collision.transform; // 공격 대상 설정
            isAttacking = true; // 이동 중단
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 공격 대상이 범위를 벗어났을 경우 해제
        if (collision.transform == currentTarget)
        {
            currentTarget = null;
            isAttacking = false; // 이동 재개
        }
    }
}
