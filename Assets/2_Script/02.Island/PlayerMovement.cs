using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       // Movement speed
    private Rigidbody2D rb;           // Rigidbody2D component
    private Animator animator;        // Animator component
    private float horizontalInput;    // Horizontal input

    private bool facingRight = true;  // 현재 오른쪽을 보고 있는지 여부

    void Start()
    {
        // Get the Rigidbody2D component and Animator component attached to the player
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get horizontal input from the player (A/D or Arrow Keys)
        horizontalInput = Input.GetAxis("Horizontal");

        // Update walking animation
        UpdateWalkingAnimation();

        // 방향 전환 확인
        FlipCheck();
    }

    void FixedUpdate()
    {
        // Move the player horizontally
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void FlipCheck()
    {
        // 오른쪽으로 이동 중이고 왼쪽을 보고 있다면 플립
        if (horizontalInput < 0 && !facingRight)
        {
            Flip();
        }
        // 왼쪽으로 이동 중이고 오른쪽을 보고 있다면 플립
        else if (horizontalInput > 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // 현재 보는 방향 반전
        facingRight = !facingRight;

        // X축의 스케일 반전
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void UpdateWalkingAnimation()
    {
        // 플레이어가 움직이고 있는지 확인
        bool isWalking = Mathf.Abs(horizontalInput) > 0.1f;

        // 애니메이터의 isWalking 파라미터 업데이트
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
        }
    }
}
