using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float delay;

    private bool canShoot = true;

    private Animator animator;
    private PlayerMove playerMove;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        Shoot();
        SlowMotion();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            if (!playerMove.isJump)
            {
                canShoot = false;
                Invoke("CanShootOn", delay);

                playerMove.canMove = false;

                animator.SetTrigger("GunShoot");
            }
            else
            {
                
            }
        }
    }

    private void SlowMotion()
    {
        if (Input.GetMouseButton(1))
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void CanShootOn()
    {
        canShoot = true;
    }

    private void CanMoveOn()
    {
        playerMove.canMove = true;
    }
}
