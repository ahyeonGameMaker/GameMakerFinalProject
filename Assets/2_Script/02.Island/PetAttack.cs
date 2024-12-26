using UnityEngine;

public class PetAttack : MonoBehaviour
{
    public int attackDamage = 10; // ���ݷ�
    public float attackCooldown = 1f; // ���� ��Ÿ��
    public float detectionRange = 5f; // Ž�� ����

    private float attackTimer; // ���� ��Ÿ�� Ÿ�̸�
    private Transform currentTarget; // ���� Ž���� ��
    private Transform attackTarget; // ���� ���� ���� ��
    private PetMovement petMovement; // PetMovement ��ũ��Ʈ ����
    private Animator animator; // Animator ������Ʈ

    void Start()
    {
        attackTimer = 0f; // Ÿ�̸� �ʱ�ȭ
        petMovement = GetComponentInParent<PetMovement>(); // PetMovement ����
        animator = GetComponentInParent<Animator>(); // Animator ������Ʈ ��������

        if (animator == null)
        {
            Debug.LogError("Animator ������Ʈ�� �ʿ��մϴ�.");
        }
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        // Ž�� �� �̵�/���� ó��
        DetectAndHandleTarget();
    }

    private void DetectAndHandleTarget()
    {
        // �� Ž��
        DetectTarget();

        if (attackTarget != null)
        {
            // ���� ���� ���� ���� ������ ����
            petMovement.StopMovement();
            if (attackTimer <= 0f)
            {
                Attack();
                attackTimer = attackCooldown; // ��Ÿ�� �ʱ�ȭ
            }
        }
        else if (currentTarget != null)
        {
            // Ž���� ������ �̵�
            petMovement.MoveToTarget(currentTarget.position);
        }
        else
        {
            // Ÿ���� ������ �÷��̾� ����ٴ�
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
            if (hit.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestTarget = hit.transform;
                    closestDistance = distance;
                }
            }
        }

        currentTarget = closestTarget; // Ž���� �� �� ���� ����� �� ����
    }

    private void Attack()
    {
        if (attackTarget != null)
        {
            Debug.Log($"���� �� {attackTarget.name}��(��) ����!");

            // �ִϸ������� Attack Ʈ���� Ȱ��ȭ
            if (animator != null)
            {
                animator.SetTrigger("IsAttack");
            }

            var targetHealth = attackTarget.GetComponent<Enemy>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage); // ü�� ����
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ������ ���� �� ����
        if (collision.CompareTag("Enemy"))
        {
            attackTarget = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ���� ������ ��� �� ����
        if (collision.transform == attackTarget)
        {
            attackTarget = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Ž�� ���� �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}