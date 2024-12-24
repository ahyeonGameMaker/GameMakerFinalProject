using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StellaBoss : MonoBehaviour
{
    public BossBullet bossBulletPrefab;
    public GameObject shootPoint;
    public float shootSpeed;

    public List<BossBullet> bossBulletPooling = new List<BossBullet>();

    public float shootPointRollSpeed;
    float rotationTimer;

    public bool isShooting;

    float shootTimer;

    public Image hpBarImage;
    public Image hpBarSecondImage;

    float stopShootTimer;

    public float hp;
    public float maxHp;

    Coroutine smoothHpBar;

    private void Start()
    {
        hp = maxHp;
    }

    private void Update()
    {
        if (isShooting)
        {
            stopShootTimer += Time.deltaTime;
            shootTimer += Time.deltaTime;
            rotationTimer += Time.deltaTime;

            float rotationAngle = Mathf.Lerp(-90, -270, Mathf.PingPong(rotationTimer * shootPointRollSpeed, 1));
            shootPoint.transform.localRotation = Quaternion.Euler(0, 0, rotationAngle);
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootSpeed)
            {
                Shoot();
                shootTimer = 0;
            }
            if (stopShootTimer >= 5)
            {
                EndShoot();
                stopShootTimer = 0;
            }
        }
        
    }

    public void StartShoot()
    {
        isShooting = true;
        StellaGameMgr.Instance.mission.SetActive(false);
    }

    public void EndShoot()
    {
        isShooting = false;
        shootPoint.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        StellaGameMgr.Instance.RoundStart();
    }

    public void Shoot()
    {
        BossBullet bullet = null;

        for (int i = 0; i < bossBulletPooling.Count; i++)
        {
            if (!bossBulletPooling[i].gameObject.activeSelf)
            {
                bullet = bossBulletPooling[i];
                break;
            }
        }

        if (bullet == null)
        {
            bullet = Instantiate(bossBulletPrefab);
            bossBulletPooling.Add(bullet);
        }

        ActivateBullet(bullet);
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
    public void TakeDamage(float damge)
    {
        hp -= damge;
        hpBarImage.fillAmount = hp / maxHp;
        if (smoothHpBar != null)
        {
            StopCoroutine(smoothHpBar);
            smoothHpBar = null;
        }
        smoothHpBar = StartCoroutine(CoSmoothHpBar(hpBarImage.fillAmount, 1));
    }

    void ActivateBullet(BossBullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.gameObject.transform.position = shootPoint.transform.position;
        bullet.gameObject.transform.localRotation = shootPoint.transform.localRotation;
    }
}
