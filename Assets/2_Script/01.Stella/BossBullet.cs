using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage;

    public float moveSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StellaPlayer player = collision.GetComponent<StellaPlayer>();
            player.TakeDamge(damage);
            gameObject.SetActive(false);
        }else if (collision.gameObject.CompareTag("RemoveWall"))
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

}
