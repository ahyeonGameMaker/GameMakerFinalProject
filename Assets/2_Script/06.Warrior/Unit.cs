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
    Coroutine takeDamageColorChange;
    public Image hpBarImage;
    public Image hpBarSecondImage;
    public int damage;
    Collider2D[] hitTargets;
    public float hitTime;
    public float maxHitTime;
    AnimationEventHandler animationEventHandler;
    bool die;
    public bool knockDown;
    public Image fKeyImage;

    public GameObject FighterObject { get => gameObject; }

    public float fKeyImageRange;
    private void OnEnable()
    {
        hp = maxHp;
    }

    private void Start()
    {
        animationEventHandler = GetComponentInChildren<AnimationEventHandler>();
        animationEventHandler.startAttackListener += StartAttack;
        animationEventHandler.dieAttackListener += Die;
    }

    private void Update()
    {
        if (die)
            return;
        if (knockDown)
        {
            Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, fKeyImageRange, 0);
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i].CompareTag("Player"))
                {
                    fKeyImage.gameObject.SetActive(true);
                }
            }
            return;
        }
           

       

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
        if(takeDamageColorChange != null)
        {
            StopCoroutine(takeDamageColorChange);
            takeDamageColorChange = null;
        }
        if (smoothHpBar != null)
        {
            StopCoroutine(smoothHpBar);
            smoothHpBar = null;
        }
        smoothHpBar = StartCoroutine(CoSmoothHpBar(hpBarImage.fillAmount, 1));
        takeDamageColorChange = StartCoroutine(CoTakeDamageColorChange());

        

        if (hp <= 0)
        {
            knockDown = false;
            die = true;
            animator.Play("Die");
        }

        if (hp / maxHp <= 0.2f && !die && unitType == UnitType.Enemy)
        {
            animator.Play("Idle");
            knockDown = true;
        }
    }

    public void Catch()
    {
        gameObject.layer = LayerMask.NameToLayer("Friendly");
        unitType = UnitType.Friendly;
        knockDown = false;
        hp = maxHp;
        hpBarImage.fillAmount = hp / maxHp;
        hpBarSecondImage.fillAmount = hpBarImage.fillAmount;
        hpBarImage.color = Color.green;
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    IEnumerator CoTakeDamageColorChange()
    {
        body.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        body.GetComponent<SpriteRenderer>().color = Color.white;
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fKeyImageRange);
    }
}

public enum UnitType
{
    Friendly,
    Enemy
}
