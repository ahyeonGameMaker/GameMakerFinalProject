using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour, IFighter
{
    public UnitType unitType;
    public GameObject body;
    public Animator animator;

    public LayerMask targetLayer;

    public float maxHp;
    public float hp;

    public float attackRange;

    public GameObject attackPoint;
    public float moveSpeed;

    public GameObject target;
    Coroutine smoothHpBar;
    public Image hpBarImage;
    public Image hpBarSecondImage;
    public int damage;
    Collider2D[] hitTargets;
    public float hitTime;
    public float maxHitTime;
    bool canAttack;
    AnimationEventHandler animationEventHandler;

    public GameObject FighterObject { get => gameObject; }
    private void OnEnable()
    {
        hp = maxHp;
    }

    private void Start()
    {
        animationEventHandler = GetComponentInChildren<AnimationEventHandler>();
        animationEventHandler.startAttackListener += StartAttack;
    }

    private void Update()
    {
        hitTargets = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayer);
        if(hitTargets.Length <= 0)
        {
            Move();
        }
        else
        {
            if (hitTime <= 0)
            {
                animator.Play("Attack");
                hitTime = maxHitTime;
            }
            else
            {
               hitTime -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpBarImage.fillAmount = hp / maxHp;
        if (smoothHpBar != null)
        {
            StopCoroutine(smoothHpBar);
            smoothHpBar = null;
        }
        smoothHpBar = StartCoroutine(CoSmoothHpBar(hpBarImage.fillAmount, 1));
    }
    private IEnumerator CoSmoothHpBar(float targetFillAmount, float duration)
    {
        float elapsedTime = 0f;
        float startFillAmount = hpBarSecondImage.fillAmount;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            hpBarSecondImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime / duration);
            yield return null;
        }

        hpBarSecondImage.fillAmount = targetFillAmount;
    }
    private void Move()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, GameMgr.Instance.targettingRange, targetLayer);

        if (targets.Length > 0)
        {
            Collider2D closestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (Collider2D target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            if (closestTarget != null)
            {
                animator.Play("Move");
                Vector2 direction = (closestTarget.transform.position - transform.position);
                direction = new Vector2(direction.x, 0).normalized;
                target = closestTarget.gameObject;
                if (closestTarget.transform.position.x < transform.position.x)
                {
                    body.transform.localScale = new Vector3(-Mathf.Abs(body.transform.localScale.x), body.transform.localScale.y, body.transform.localScale.z);
                }
                else
                {
                    body.transform.localScale = new Vector3(Mathf.Abs(body.transform.localScale.x), body.transform.localScale.y, body.transform.localScale.z);
                }
                transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                animator.Play("Idle");
            }
        }
    }

    public void StartAttack()
    {
        if (hitTargets.Length > 0)
        {
            Collider2D closestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (Collider2D target in hitTargets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            if (closestTarget != null)
            {
                closestTarget.GetComponent<IFighter>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}

public enum UnitType
{
    Friendly,
    Enemy
}
