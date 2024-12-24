using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private Rigidbody2D rb;     // Rigidbody2D component
    private float horizontalInput; // Horizontal input

    private bool facingRight = true; // ���� �������� ���� �ִ��� ����

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get horizontal input from the player (A/D or Arrow Keys)
        horizontalInput = Input.GetAxis("Horizontal");

        // ���� ��ȯ Ȯ��
        FlipCheck();
    }

    void FixedUpdate()
    {
        // Move the player horizontally
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void FlipCheck()
    {
        // ���������� �̵� ���̰� ������ ���� �ִٸ� �ø�
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        // �������� �̵� ���̰� �������� ���� �ִٸ� �ø�
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // ���� ���� ���� ����
        facingRight = !facingRight;

        // X���� ������ ����
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
