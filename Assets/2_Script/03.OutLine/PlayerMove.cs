using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;

    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public bool canJump = true;
    [HideInInspector]
    public bool isJump = false;
    private bool isRight = true;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Flip();
        Jump();
    }

    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && canMove)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

            animator.SetBool("Run", true);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

            animator.SetBool("Run", false);
        }
    }

    private void Flip()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") > 0 && !isRight)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1; // X축 스케일을 반전
                transform.localScale = localScale;
                isRight = true; // 오른쪽을 보고 있다고 설정
            }
            else if (Input.GetAxisRaw("Horizontal") < 0 && isRight)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1; // X축 스케일을 반전
                transform.localScale = localScale;
                isRight = false; // 왼쪽을 보고 있다고 설정
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canMove && canJump)
        {
            canJump = false;
            isJump = true;

            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

            animator.SetBool("Run", false);
            animator.SetTrigger("Jump");
        }
    }
}
