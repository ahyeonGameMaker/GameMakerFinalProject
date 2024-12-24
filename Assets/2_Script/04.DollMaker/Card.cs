using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardRenerer;
    [SerializeField] private Sprite PartSprite;
    [SerializeField] private Sprite backSprite;
    private bool isFlipped = false;
    private bool isFlipping = false;
    private bool isMatched = false;
    public int cardID;
    public AudioSource CardSource;
    public void SetCardID(int id)
    {
        cardID = id;
    }
    public void SetMatched()
    {
        isMatched = true;
    }
    public void SetPartSprite(Sprite sprite)
    {
        PartSprite = sprite;
    }
    public void FilpCard()
    {
        isFlipping = true;

        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(0f, originalScale.y, originalScale.z);

        transform.DOScale(targetScale, 0.2f).OnComplete(() =>
        {
            isFlipped = !isFlipped;

            if (isFlipped)
            {
                cardRenerer.sprite = PartSprite;
            }
            else 
            {
                cardRenerer.sprite = backSprite;    
            }
            transform.DOScale(originalScale, 0.2f);
        });
        isFlipping = false;
    }
    void OnMouseDown()
     {
         if(!isFlipping && !isMatched && !isFlipped)
         {
             GameManager.instance.CardClicked(this);
            CardSource.Play();
            Debug.Log("CardCilck");
         }
        else
        {
            Debug.Log("Card not clickable");  // 클릭이 안되었을 때
        }
    }
  
}
