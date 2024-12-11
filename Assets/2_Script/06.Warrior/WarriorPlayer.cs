using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    public LayerMask targetLayer;

    public GameObject FighterObject { get => gameObject; }

    private void Start()
    {
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
                GameMgr.Instance.attackImage.sprite = GameMgr.Instance.firstAttackSprite;
            }
            else if (attackCount == 1)
            {
                GameMgr.Instance.attackImage.sprite = GameMgr.Instance.secondAttackSprite;
            }
            else if (attackCount == 2)
            {
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
        smoothHpBar = StartCoroutine(CoSmoothHpBar(hpBarImage.fillAmount, 1));
    }
    void EndAttack()
    {

    }

    public void StartAttack()
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll(attackPoint.transform.position, attackRange, targetLayer);
        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].gameObject.GetComponent<IFighter>().TakeDamage(damage);
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
