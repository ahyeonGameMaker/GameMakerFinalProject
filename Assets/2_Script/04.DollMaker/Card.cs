using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardRenerer;
    [SerializeField] private Sprite PartSprite;
    [SerializeField] private Sprite backSprite;
    private bool isFlipped = false;
    private bool isFlipping = false;
    public int cardID;

    public void SetCardID(int id)
    {
        cardID = id;
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

    }
    void OnMouseDown()
    {
        if(!isFlipping)
        {
            FilpCard();
        }
    }
}
