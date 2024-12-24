using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int gold;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GoldManager.instance.AddGold(gold);
            Destroy(gameObject);
        }
    }
}
