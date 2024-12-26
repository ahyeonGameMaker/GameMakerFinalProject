using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StellaPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    Camera mainCamera;
    public float hp;
    public float maxHp;

    public Image hpBarImage;
    public Image hpBarSecondImage;

    Coroutine smoothHpBar;

    AudioSource audioSource;

    public bool stop;

    private void Start()
    {
        hp = maxHp;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    public void TakeDamge(float damage)
    {
        audioSource.Play();
        hp -= damage;
        hpBarImage.fillAmount = hp / maxHp;
        if (smoothHpBar != null)
        {
            StopCoroutine(smoothHpBar);
            smoothHpBar = null;
        }
        smoothHpBar = StartCoroutine(CoSmoothHpBar(hpBarImage.fillAmount, 1));
        if(hp <= 0)
        {
            stop = true;
            StellaGameMgr.Instance.NextScene(false);
        }
    }

    private void Update()
    {
        if (!stop)
        {
            rb.velocity = Vector2.zero;
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector2 moveDirection = new Vector2(moveX, moveY);
            if (moveDirection.sqrMagnitude > 0.01f)
            {
                rb.velocity = moveDirection.normalized * moveSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

            rb.velocity = moveDirection * moveSpeed;
        }
        ClampPlayerPosition();
    }

    private IEnumerator CoSmoothHpBar(float targetFillAmount, float duration)
    {
        float elapsedTime = 0f;
        float startFillAmount = hpBarSecondImage.fillAmount;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            hpBarSecondImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime / duration);
            yield return null;
        }

        hpBarSecondImage.fillAmount = targetFillAmount;
    }

    void ClampPlayerPosition()
    {
        Vector3 playerPosition = transform.position;

        Vector3 minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        playerPosition.x = Mathf.Clamp(playerPosition.x, minBounds.x, maxBounds.x);
        playerPosition.y = Mathf.Clamp(playerPosition.y, minBounds.y, maxBounds.y);

        transform.position = playerPosition;
    }
}
