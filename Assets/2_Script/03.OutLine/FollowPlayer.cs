using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 2f; // ���� �̵� �ӵ�
    private Transform player; // �÷��̾��� Transform
    private SpriteRenderer spriteRenderer; // ���� SpriteRenderer

    void Start()
    {
        // �÷��̾� ã�� (�±׷� �˻�)
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // SpriteRenderer ��������
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            // �÷��̾��� X ��ġ ���󰡱�
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // FlipX ����
            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false; // �÷��̾ �����ʿ� ����
            }
            else
            {
                spriteRenderer.flipX = true; // �÷��̾ ���ʿ� ����
            }
        }
    }
}
