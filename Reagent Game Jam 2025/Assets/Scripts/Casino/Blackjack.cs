using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class BlackjackGame : MonoBehaviour
{
    public GameObject wagerPanel;
    public GameObject hitAndStandPanel;
    public TMP_Text wagerText;
    public Button increaseWagerButton;
    public Button decreaseWagerButton;

    private int currentWager = 0;
    private int minWager = 10;
    private int maxWager = 500;
    private int wagerStep = 10;
    
    public TMP_Text playerScoreText;
    public TMP_Text dealerScoreText;
    public TMP_Text resultText;
    
    public Button backToMenuButton;
    public Button playAgainButton;
    public Button playButton;
    
    private List<int> playerCards;
    private List<int> dealerCards;
    private bool isGameOver;

    public Sprite[] cardSprites;
    public Image[] playerCardImages;
    public Image[] dealerCardImages;

    public void Start()
    {
        currentWager = minWager;
        UpdateWagerUI();
        
        wagerPanel.SetActive(true);
        
        hitAndStandPanel.gameObject.SetActive(false);
        backToMenuButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        if (playerCards != null)
            ResetCards();

        isGameOver = false;

        playerCards = new List<int>();
        dealerCards = new List<int>();

        playerCards.Add(DrawCard());
        playerCards.Add(DrawCard());
        dealerCards.Add(DrawCard());
        dealerCards.Add(DrawCard());

        ShowCards();
        UpdateUI();
    }



    public void Hit()
    {
        if (isGameOver) return;

        playerCards.Add(DrawCard());  
        ShowCards(); 
        UpdateUI();

        if (GetScore(playerCards) > 21)
        {
            EndGame("Bust! Dealer wins.");
        }
    }

    public void Stand()
    {
        if (isGameOver) return;

        StartCoroutine(DealerTurn()); 
    }
    
    IEnumerator DealerTurn()
    {
        while (GetScore(dealerCards) < 17)
        {
            dealerCards.Add(DrawCard());
            ShowCards();
            UpdateUI();
            yield return new WaitForSeconds(1f);  
        }

        int playerScore = GetScore(playerCards);
        int dealerScore = GetScore(dealerCards);

        if (dealerScore > 21 || playerScore > dealerScore)
        {
            EndGame("You win!");
        }
        else if (dealerScore == playerScore)
        {
            EndGame("Draw.");
        }
        else
        {
            EndGame("Dealer wins.");
        }
    }

    void EndGame(string result)
    {
        isGameOver = true;
        resultText.text = result;
        resultText.gameObject.SetActive(true);
    
        backToMenuButton.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);

        if (result == "You win!")
        {
            GameManager.Instance.AddMoney(currentWager * 2);
        }
        else if (result == "Draw.")
        {
            GameManager.Instance.AddMoney(currentWager); 
        }

        UpdateUI();
    }


    int DrawCard()
    {
        return Random.Range(1, 11);
    }
    
    void ShowCards()
    {
        for (int i = 0; i < playerCards.Count; i++)
        {
            if (i < playerCardImages.Length)
            {
                playerCardImages[i].sprite = cardSprites[playerCards[i] - 1]; 
                playerCardImages[i].gameObject.SetActive(true);  
            }
        }

        for (int i = 0; i < dealerCards.Count; i++)
        {
            if (i < dealerCardImages.Length)
            {
                dealerCardImages[i].sprite = cardSprites[dealerCards[i] - 1];  
                dealerCardImages[i].gameObject.SetActive(true); 
            }
        }
    }



    int GetScore(List<int> hand)
    {
        int total = 0;
        foreach (var card in hand)
        {
            total += card;
        }
        return total;
    }

    void UpdateUI()
    {
        playerScoreText.text = "Player: " + GetScore(playerCards);
        dealerScoreText.text = "Dealer: " + GetScore(dealerCards);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    void ResetCards()
    {
        foreach (Image cardImage in playerCardImages)
        {
            cardImage.gameObject.SetActive(false);  
        }

        foreach (Image cardImage in dealerCardImages)
        {
            cardImage.gameObject.SetActive(false); 
        }
        
        playerCards.Clear();
        dealerCards.Clear();
    }
    
    public void IncreaseWager()
    {
        currentWager += wagerStep;
        currentWager = Mathf.Min(currentWager, maxWager);
        UpdateWagerUI();
    }

    public void DecreaseWager()
    {
        currentWager -= wagerStep;
        currentWager = Mathf.Max(currentWager, minWager);
        UpdateWagerUI();
    }
    
    public void OnPlayButtonClicked()
    {
        if (!GameManager.Instance.SpendMoney(currentWager))
        {
            resultText.text = "Not enough money!";
            resultText.gameObject.SetActive(true);
            return;
        }

        wagerPanel.SetActive(false);  
        resultText.gameObject.SetActive(false);
        hitAndStandPanel.gameObject.SetActive(true);
        StartGame();  
    }

    public void PlayAgain()
    {
        wagerPanel.SetActive(true);
        resultText.gameObject.SetActive(false);
        ResetCards();
        resultText.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        backToMenuButton.gameObject.SetActive(false);
        hitAndStandPanel.gameObject.SetActive(false);
    }
    
    

    void UpdateWagerUI()
    {
        wagerText.text = "Wager: €" + currentWager;
    }
}
