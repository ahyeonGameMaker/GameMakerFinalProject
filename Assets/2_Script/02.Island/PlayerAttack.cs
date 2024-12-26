using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10; // 공격력
    public float attackCooldown = 1f; // 공격 간격
    public LayerMask enemyLayer; // 공격 대상 레이어

    private float attackTimer; // 현재 공격 쿨타임 타이머
    private Collider2D currentTarget; // 현재 공격 대상
    private Animator animator; // 애니메이터 컴포넌트

    void Start()
    {
        attackTimer = 0f; // 초기 타이머 설정
        animator = GetComponentInParent<Animator>(); // Animator 컴포넌트 가져오기
    }

    void Update()
    {
        // 쿨타임 감소
        attackTimer -= Time.deltaTime;

        // 공격 대상이 있을 경우 자동 공격
        if (currentTarget != null && attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown; // 쿨타임 초기화
        }
    }

    private void Attack()
    {
        Debug.Log("플레이어가 공격했습니다!");

        // 애니메이터의 Attack 트리거 활성화
        if (animator != null)
        {
            animator.SetTrigger("IsAttack");
        }

        // 현재 타겟에 데미지를 전달
        if (currentTarget != null)
        {
            // Enemy 컴포넌트에 데미지 전달
            var enemyHealth = currentTarget.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                return; // Enemy에 데미지를 전달했으면 종료
            }

            // Health 컴포넌트에 데미지 전달
            var targetHealth = currentTarget.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage);
                return; // Health에 데미지를 전달했으면 종료
            }

            Debug.LogWarning($"{currentTarget.name}는 데미지를 받을 수 있는 컴포넌트가 없습니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적 레이어를 가진 오브젝트만 공격 대상으로 설정
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            currentTarget = collision; // 타겟 설정
            Debug.Log($"적 감지: {collision.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 적이 범위를 벗어나면 타겟 해제
        if (collision == currentTarget)
        {
            currentTarget = null;
            Debug.Log($"적 범위 이탈: {collision.name}");
        }
    }
}
