using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WarriorPlayer : MonoBehaviour, IFighter
{
    public float moveSpeed;
    public float jumpPower;
    public Animator animator;
    Rigidbody2D rb;
    public GameObject body;
    bool isGrounded;
    bool isJumping;
    public LayerMask groundLayer;
    public float maxAttackTime;
    public float attackTime;
    public int attackCount;
    public int maxAttackCount;
    bool canAttack;
    AnimationEventHandler animationEventHandler;
    public GameObject attackPoint;

    public float hp;
    public float maxHp;

    public Image hpBarImage;
    public Image hpBarSecondImage;

    Coroutine smoothHpBar;

    public Vector2 attackRange;

    public int damage;
    int currentDmage;

    public LayerMask targetLayer;
    Color damageColor;
    Coroutine takeDamageColorChange;


    public GameObject FighterObject { get => gameObject; }

    private void Start()
    {
        damageColor = body.GetComponent<SpriteRenderer>().color = Color.white;
        rb = GetComponent<Rigidbody2D>();
        animationEventHandler = GetComponentInChildren<AnimationEventHandler>();
        animationEventHandler.endAttackListener += EndAttack;
        animationEventHandler.startAttackListener += StartAttack;
        hp = maxHp;
    }

    private void Update()
    {
        Attack();
        if (!canAttack)
        {
            Jump();
            Move();
        }

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
    void Attack()
    {

        if (!isGrounded)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector3.zero;
            attackTime = maxAttackTime;
            animator.Play("Attack" + attackCount);

            if (attackCount == 0)
            {
                attackPoint.transform.localPosition = new Vector2(0.271f, 0);
                attackRange = new Vector2(2.88f, 1.99f);
                currentDmage = damage;
                GameMgr.Instance.attackImage.sprite = GameMgr.Instance.firstAttackSprite;
            }
            else if (attackCount == 1)
            {
                attackPoint.transform.localPosition = new Vector2(0.155f, 0);
                attackRange = new Vector2(5.14f, 0.38f);
                currentDmage = damage * 2;
                GameMgr.Instance.attackImage.sprite = GameMgr.Instance.secondAttackSprite;
            }
            else if (attackCount == 2)
            {
                attackPoint.transform.localPosition = new Vector2(0.338f, 0.175f);
                attackRange = new Vector2(4.06f, 3.8f);
                currentDmage = damage * 3;
                GameMgr.Instance.attackImage.sprite = GameMgr.Instance.thirdAttackSprite;
            }

            if (attackCount == maxAttackCount)
            {
                attackCount = 0;
            }
            else
            {
                attackCount++;
            }

            canAttack = true;
        }

        if (canAttack)
        {
            attackTime -= Time.deltaTime;

            if (attackTime <= 0)
            {
                attackCount = 0;
                attackTime = maxAttackTime;
                GameMgr.Instance.attackImage.sprite = GameMgr.Instance.idleSprite;
                canAttack = false;
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
        if (takeDamageColorChange != null)
        {
            StopCoroutine(takeDamageColorChange);
            takeDamageColorChange = null;
        }
        smoothHpBar = StartCoroutine(CoSmoothHpBar(hpBarImage.fillAmount, 1));
        takeDamageColorChange = StartCoroutine(CoTakeDamageColorChange());
    }
    void EndAttack()
    {

    }
    IEnumerator CoTakeDamageColorChange()
    {
        body.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        body.GetComponent<SpriteRenderer>().color = damageColor;
    }

    public void StartAttack()
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll(attackPoint.transform.position, attackRange, 0);

        Debug.Log(targets.Length); 
        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++) 
            {
                Debug.Log(targets[i].name);
                if (targets[i].gameObject.GetComponent<Unit>() != null && targets[i].gameObject.GetComponent<Unit>().unitType == UnitType.Enemy) 
                {
                    targets[i].gameObject.GetComponent<IFighter>().TakeDamage(currentDmage); 
                }
                if(targets[i].gameObject.GetComponent<Stone>() != null)
                {
                    targets[i].gameObject.GetComponent<IFighter>().TakeDamage(currentDmage);
                }
            }
        }
    }

    void Move()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        float horInput = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(horInput * moveSpeed, rb.velocity.y);
        rb.velocity = dir;

        if (horInput > 0)
        {
            body.transform.localScale = new Vector3(1, 1, 1);
            if (!isJumping)
                animator.Play("Run");
        }
        else if (horInput < 0)
        {
            body.transform.localScale = new Vector3(-1, 1, 1);
            if (!isJumping)
                animator.Play("Run");
        }
        else if (horInput == 0)
        {
            if (!isJumping)
                animator.Play("Idle");
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            animator.Play("Jump");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.transform.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
            isJumping = false;
            animator.Play("Idle");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isJumping = true;
            isGrounded = false;
        }
    }
}
