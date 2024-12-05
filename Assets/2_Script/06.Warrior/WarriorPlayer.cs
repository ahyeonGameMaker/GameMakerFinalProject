using UnityEngine;

public class WarriorPlayer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;
    public Animator animator;
    Rigidbody2D rb;
    public GameObject body;
    bool isGrounded;
    bool isJumping;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Jump();
        Move();
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
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(0.5f, 0.1f, 0));
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
