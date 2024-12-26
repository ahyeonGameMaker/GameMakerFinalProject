using UnityEngine;

public class PetAttack : MonoBehaviour
{
    public int attackDamage = 10; // 공격력
    public float attackCooldown = 1f; // 공격 쿨타임
    public float detectionRange = 5f; // 탐지 범위

    private float attackTimer; // 공격 쿨타임 타이머
    private Transform currentTarget; // 현재 탐지된 적
    private Transform attackTarget; // 공격 범위 안의 적
    private PetMovement petMovement; // PetMovement 스크립트 참조
    private Animator animator; // Animator 컴포넌트

    void Start()
    {
        attackTimer = 0f; // 타이머 초기화
        petMovement = GetComponentInParent<PetMovement>(); // PetMovement 참조
        animator = GetComponentInParent<Animator>(); // Animator 컴포넌트 가져오기

        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트가 필요합니다.");
        }
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        // 탐지 및 이동/공격 처리
        DetectAndHandleTarget();
    }

    private void DetectAndHandleTarget()
    {
        // 적 탐지
        DetectTarget();

        if (attackTarget != null)
        {
            // 공격 범위 내에 적이 있으면 공격
            petMovement.StopMovement();
            if (attackTimer <= 0f)
            {
                Attack();
                attackTimer = attackCooldown; // 쿨타임 초기화
            }
        }
        else if (currentTarget != null)
        {
            // 탐지된 적에게 이동
            petMovement.MoveToTarget(currentTarget.position);
        }
        else
        {
            // 타겟이 없으면 플레이어 따라다님
            petMovement.FollowPlayer();
        }
    }

    private void DetectTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            // Enemy 태그 또는 Health 컴포넌트를 가진 오브젝트 탐지
            if (hit.CompareTag("Enemy") || hit.GetComponent<Health>() != null)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestTarget = hit.transform;
                    closestDistance = distance;
                }
            }
        }

        currentTarget = closestTarget; // 탐지된 적 중 가장 가까운 적 설정
    }

    private void Attack()
    {
        if (attackTarget != null)
        {
            Debug.Log($"펫이 적 {attackTarget.name}을(를) 공격!");

            // 애니메이터의 Attack 트리거 활성화
            if (animator != null)
            {
                animator.SetTrigger("IsAttack");
            }

            // Enemy 또는 Health에 데미지 전달
            var enemyComponent = attackTarget.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
                return;
            }

            var healthComponent = attackTarget.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(attackDamage);
                return;
            }

            Debug.LogWarning($"{attackTarget.name}는 데미지를 받을 수 있는 컴포넌트가 없습니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 공격 범위에 들어온 적 설정
        if (collision.CompareTag("Enemy") || collision.GetComponent<Health>() != null)
        {
            attackTarget = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 공격 범위를 벗어난 적 해제
        if (collision.transform == attackTarget)
        {
            attackTarget = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 탐지 범위 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
