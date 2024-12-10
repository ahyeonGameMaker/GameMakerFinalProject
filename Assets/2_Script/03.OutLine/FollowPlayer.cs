using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 2f; // 적의 이동 속도
    private Transform player; // 플레이어의 Transform
    private SpriteRenderer spriteRenderer; // 적의 SpriteRenderer

    void Start()
    {
        // 플레이어 찾기 (태그로 검색)
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // SpriteRenderer 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            // 플레이어의 X 위치 따라가기
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // FlipX 설정
            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false; // 플레이어가 오른쪽에 있음
            }
            else
            {
                spriteRenderer.flipX = true; // 플레이어가 왼쪽에 있음
            }
        }
    }
}
