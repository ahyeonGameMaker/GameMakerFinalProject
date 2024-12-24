using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UIManager.instance.GaemOverUI();
            if (ScoreManager.instance.score >= 5)
            {
                UIManager.instance.nextBtn.SetActive(true);
            }
            EnemySpawner.instance.GameOver();
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            ScoreManager.instance.score += 1;
            Destroy(gameObject);
        }
    }
}
