using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<Card> allCards;
    private Card flippedCard;
    private bool isFlipping = false;

    [SerializeField] private Slider timeoutSlider;
    [SerializeField] private TextMeshProUGUI timeoutText;
    [SerializeField] private float timeLimit = 60f;
    private float currentTime;

    private int totalMatches = 6;
    private int matchesFound = 0;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    private bool isGameOver = false;

    [SerializeField] private AudioSource S;
    [SerializeField] private AudioSource F;
    [SerializeField] private AudioSource G_F;
    [SerializeField] private AudioSource G_S;

    bool success;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Board board = FindAnyObjectByType<Board>();
        allCards = board.GetCards();

        currentTime = timeLimit;
        SetCurrentTimeText();
        StartCoroutine("FlipAllCardsRoutine");
    }
    void SetCurrentTimeText()
    {
        int timeSec = Mathf.CeilToInt(currentTime);
        timeoutText.SetText(timeSec.ToString());
    }
    IEnumerator FlipAllCardsRoutine()
    {
        isFlipping  = true; 
        yield return new WaitForSeconds(0.5f);
        FlipAllCards();
        yield return new WaitForSeconds(3f);
        FlipAllCards();
        yield return new WaitForSeconds(0.5f);
        isFlipping = false;

        yield return StartCoroutine("CountDownTimerRoutine");
    }
    IEnumerator CountDownTimerRoutine()
    {
        while(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeoutSlider.value = currentTime/timeLimit;
            SetCurrentTimeText() ;
            yield return null;
        }
        GameOver(false);
    }
    void FlipAllCards()
    {
        foreach(Card card in allCards)
        {
            card.FilpCard();
        }
    }
    public void CardClicked(Card card)
    {
        if (isFlipping || isGameOver)
        {
            return;
        }

        card.FilpCard();

        if(flippedCard == null)
        {
            flippedCard = card;
        }
        else
        {
            StartCoroutine(CheckMatchRoutine(flippedCard, card));
        }
    }
    IEnumerator CheckMatchRoutine(Card card1, Card card2)
    {
        isFlipping = true;

        if(card1.cardID == card2.cardID)
        {
            card1.SetMatched();
            card2.SetMatched();
            S.Play();

            matchesFound++;

            if(matchesFound == totalMatches)
            {
                GameOver(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(1f);

            card1.FilpCard();
            card2.FilpCard();
            F.Play();

            yield return new WaitForSeconds (0.4f);
        }
        isFlipping = false;
        flippedCard = null;
    }
    void GameOver(bool success)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            this.success = success;
            StopCoroutine("CountDownTimerRoutine");

            if (success)
            {
                gameOverText.SetText("Game Clear");
                G_S.Play();
            }
            else
            {
                gameOverText.SetText("Game Over");
                G_F.Play();
            }
            Invoke("ShowGameOverPanel", 2f);
        }
    }
    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    public void Restart()
    {
        TopBarManager.Instance.LoadScene(2);
        if(success)
            TopBarManager.Instance.EndGame(1);
    }

}
