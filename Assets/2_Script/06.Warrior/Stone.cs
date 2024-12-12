using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stone : MonoBehaviour, IFighter
{
    public Image hpBarImage;
    public Image hpBarSecondImage;
    public float maxHp;
    public float hp;
    Coroutine smoothHpBar;
    Coroutine takeDamageColorChange;
    Color damageColor;
    public TMP_Text hpText;
    public GameObject FighterObject { get => gameObject; }
    void Start()
    {
        hp = maxHp;
        hpBarImage.fillAmount = hp / maxHp;
        damageColor = GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpBarImage.fillAmount = hp / maxHp;
        hpText.text = hp + "/" + maxHp;
        if (hp <= 0)
        {
            damageColor = GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (takeDamageColorChange != null)
        {
            StopCoroutine(takeDamageColorChange);
            takeDamageColorChange = null;
        }
        if (smoothHpBar != null)
        {
            StopCoroutine(smoothHpBar);
            smoothHpBar = null;
        }

        if (gameObject.activeInHierarchy)
        {
            smoothHpBar = StartCoroutine(CoSmoothHpBar(hpBarImage.fillAmount, 1));
            takeDamageColorChange = StartCoroutine(CoTakeDamageColorChange());
        }

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

    IEnumerator CoTakeDamageColorChange()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = damageColor;
    }

}
