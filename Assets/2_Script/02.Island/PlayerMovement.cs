using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private Rigidbody2D rb;     // Rigidbody2D component
    private float horizontalInput; // Horizontal input

    private bool facingRight = true; // 현재 오른쪽을 보고 있는지 여부

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get horizontal input from the player (A/D or Arrow Keys)
        horizontalInput = Input.GetAxis("Horizontal");

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
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        // 왼쪽으로 이동 중이고 오른쪽을 보고 있다면 플립
        else if (horizontalInput < 0 && facingRight)
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
}
