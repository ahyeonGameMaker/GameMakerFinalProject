using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public PlayerMove playerMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && !playerMove.canJump)
        {
            playerMove.canJump = true;
            playerMove.isJump = false;
        }
    }
}
