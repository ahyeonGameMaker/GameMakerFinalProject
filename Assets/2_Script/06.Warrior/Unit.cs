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
    public LayerMask friendlyLayer;
    public LayerMask enemyLayer;

    public float maxHp;
    public float hp;

    public Vector2 attackRange;

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
    public AudioSource attacksound;
    public AudioSource AttackSound { get => attacksound; }
    Color damageColor;

    public GameObject FighterObject { get => gameObject; }

    public float fKeyImageRange;

    public bool wideUnit;
    bool isAttack;
    bool isAttacking;

    public EnemyType enemyType;
    private void OnEnable()
    {
        hpBarImage.color = Color.red;
        hp = maxHp;
        hpBarImage.fillAmount = hp / maxHp;
        hpBarSecondImage.fillAmount = hpBarImage.fillAmount;
        damageColor = body.GetComponent<SpriteRenderer>().color = Color.white;
        knockDown = false;
        die = false;
        targetLayer = friendlyLayer;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        gameObject.tag = "Enemy";
        unitType = UnitType.Enemy;
        fKeyImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        animationEventHandler = GetComponentInChildren<AnimationEventHandler>();
        animationEventHandler.startAttackListener += StartAttack;
        animationEventHandler.dieAttackListener += Die;
        animationEventHandler.endAttackListener += EndAttack;
    }

    private void Update()
    {
        if (die)
            return;
        if (knockDown)
        {
            Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, fKeyImageRange, targetLayer);
            Debug.Log(players.Length);
            bool isPlayer = false;
            if(players.Length == 0)
            {
                isPlayer = false;
            }
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i].CompareTag("Player"))
                {
                    isPlayer = true;
                    break;
                }
                isPlayer = false;
            }

            if (!isPlayer)
            {
                fKeyImage.gameObject.SetActive(false);
            }
            else
            {
                fKeyImage.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Catch();
                }
            }
            return;
        }

        if(target != null)
        {
            Vector2 direction = (target.transform.position - transform.position);
            direction = new Vector2(direction.x, 0).normalized;

            if (direction.x < 0)
            {
                body.transform.localScale = new Vector3(-Mathf.Abs(body.transform.localScale.x), body.transform.localScale.y, body.transform.localScale.z);
            }
            else if (direction.x > 0)
            {
                body.transform.localScale = new Vector3(Mathf.Abs(body.transform.localScale.x), body.transform.localScale.y, body.transform.localScale.z);
            }
        }
        


        hitTargets = Physics2D.OverlapBoxAll(transform.position, attackRange, 0);
        foreach (var target in hitTargets)
        {
            if (unitType == UnitType.Enemy)
            {
                if (target.gameObject.CompareTag("Player") || target.gameObject.CompareTag("Friendly"))
                {
                    isAttack = true;
                    this.target = target.gameObject;
                    if (!isAttacking)
                    {
                        animator.Play("Idle");
                        isAttacking = true;
                    }
                    
                    break;
                }
            }
            else
            {
                if (target.gameObject.CompareTag("Enemy"))
                {
                    isAttack = true;
                    this.target = target.gameObject;
                    if (!isAttacking)
                    {
                        animator.Play("Idle");
                        isAttacking = true;
                    }
                    break;
                }
            }
            isAttack = false;
        }

        if (isAttack)
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
        else
        {
            isAttacking = false;
            Move();
        }
    }

    public void TakeDamage(float damage, AudioSource attackSound)
    {
        attackSound.Play();
        hp -= damage;
        hpBarImage.fillAmount = hp / maxHp;
        if (hp <= 0)
        {
            damageColor = body.GetComponent<SpriteRenderer>().color = Color.white;
            body.GetComponent<SpriteRenderer>().color = Color.white;
            knockDown = false;
            die = true;
            animator.Play("Die");
        }
        if (hp / maxHp <= 0.2f && !die && unitType == UnitType.Enemy)
        {
            damageColor = Color.green;
            body.GetComponent<SpriteRenderer>().color = Color.green;
            animator.Play("Idle");
            knockDown = true;
        }

        if (takeDamageColorChange != null)
        {
            StopCoroutine(takeDamageColorChange);
            takeDamageColorChange = null;
        }
        if (smoothHpBar != null)
        {
            StopCoroutine(smoothHpBar);
            smoothHpBar = null;
        }

        if (gameObject.activeInHierarchy)
        {
            smoothHpBar = StartCoroutine(CoSmoothHpBar(hpBarImage.fillAmount, 1));
            takeDamageColorChange = StartCoroutine(CoTakeDamageColorChange());
        }
            
    }

    public void Catch()
    {
        fKeyImage.gameObject.SetActive(false);
        damageColor = body.GetComponent<SpriteRenderer>().color = Color.white;
        body.GetComponent<SpriteRenderer>().color = Color.white;
        targetLayer = enemyLayer;
        gameObject.layer = LayerMask.NameToLayer("Friendly");
        gameObject.tag = "Friendly";
        unitType = UnitType.Friendly;
        knockDown = false;
        hp = maxHp;
        hpBarImage.fillAmount = hp / maxHp;
        hpBarSecondImage.fillAmount = hpBarImage.fillAmount;
        hpBarImage.color = Color.green;
    }

    void Die()
    {
        if (takeDamageColorChange != null)
        {
            StopCoroutine(takeDamageColorChange);
            takeDamageColorChange = null;
        }
        if (smoothHpBar != null)
        {
            StopCoroutine(smoothHpBar);
            smoothHpBar = null;
        }
        gameObject.SetActive(false);
    }

    IEnumerator CoTakeDamageColorChange()
    {
        body.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        body.GetComponent<SpriteRenderer>().color = damageColor;
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
        if (isAttack)
            return;

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
                    this.target = target.gameObject;
                }
            }

            if (closestTarget != null)
            {
                animator.Play("Move");
                Vector2 direction = (closestTarget.transform.position - transform.position);
                direction = new Vector2(direction.x, 0).normalized;
                transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                animator.Play("Idle");
            }
        }
    }

    public void EndAttack()
    {
        animator.Play("Idle");
    }
    public void StartAttack()
    {
        if (hitTargets.Length > 0)
        {
            if (!wideUnit)
            {
                Collider2D closestTarget = null;
                float closestDistance = float.MaxValue;

                foreach (Collider2D target in hitTargets)
                {
                    if (unitType == UnitType.Enemy)
                    {
                        if (!target.gameObject.CompareTag("Player") && !target.gameObject.CompareTag("Friendly"))
                            continue;
                    }
                    else if (unitType == UnitType.Friendly)
                    {
                        if (!target.gameObject.CompareTag("Enemy"))
                            continue;
                    }
                    float distance = Vector3.Distance(transform.position, target.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = target;
                    }
                }

                if (closestTarget != null)
                {
                    if (closestTarget.GetComponent<IFighter>() != null)
                        closestTarget.GetComponent<IFighter>().TakeDamage(damage, attacksound);
                }
            }
            else
            {
                for(int i = 0; i < hitTargets.Length; i++)
                {
                    if (unitType == UnitType.Enemy)
                    {
                        if (!hitTargets[i].gameObject.CompareTag("Player") && !hitTargets[i].gameObject.CompareTag("Friendly"))
                            continue;
                    }
                    else if (unitType == UnitType.Friendly)
                    {
                        if (!hitTargets[i].gameObject.CompareTag("Enemy"))
                            continue;
                    }

                    if (hitTargets[i].GetComponent<IFighter>() != null)
                    {
                        hitTargets[i].GetComponent<IFighter>().TakeDamage(damage, attacksound);
                    }
                        
                }
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fKeyImageRange);
    }
}

public enum UnitType
{
    Friendly,
    Enemy
}
