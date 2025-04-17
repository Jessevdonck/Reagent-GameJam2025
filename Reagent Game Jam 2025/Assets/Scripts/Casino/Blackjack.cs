using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class BlackjackGame : MonoBehaviour
{
    public TMP_Text playerScoreText;
    public TMP_Text dealerScoreText;
    public TMP_Text resultText;
    
    public Button backToMenuButton;
    public Button playAgainButton;
    
    private List<int> playerCards;
    private List<int> dealerCards;
    private bool isGameOver;

    public Sprite[] cardSprites;
    public Image[] playerCardImages;
    public Image[] dealerCardImages;

    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        resultText.gameObject.SetActive(false);
        backToMenuButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        
        if(playerCards != null)
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

}
