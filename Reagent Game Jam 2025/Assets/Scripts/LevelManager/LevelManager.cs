using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public List<MinigameData> minigames;

    public TextMeshProUGUI timerText;
    public float maxTimeInMinutes = 5f;
    private float currentTime;
    private bool timerRunning = false;
    private int reward = 100;
    public int minReward = 20;
    public int maxReward = 80;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();

            if (currentTime <= 0)
            {
                currentTime = 0;
                timerRunning = false;
                EndLevel();
            }
        }
    }

    public void StartTimer()
    {
        currentTime = maxTimeInMinutes * 60f;
        timerRunning = true;
        Debug.Log("Timer gestart! Tijd ingesteld op: " + currentTime);
    }
    
    public void StopTimer()
    {
        CalculateReward();
        timerRunning = false;
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void EndLevel()
    {
        CalculateReward();
    }

    private void CalculateReward()
    {
        float timeLeft = currentTime;
        reward = Mathf.FloorToInt(timeLeft/3);
        Debug.Log("Reward is: "+ reward);
        reward = Mathf.Clamp(reward, minReward, maxReward);
    }

    public void MarkMinigameComplete(int id)
    {
        var game = minigames.Find(m => m.minigameID == id);
        if (game != null)
        {
            game.isCompleted = true;
        }
    }

    public bool AreAllMinigamesCompleted()
    {
        foreach (var game in minigames)
        {
            if (!game.isCompleted)
                return false;
        }
        return true;
    }

    public bool IsMinigameCompleted(int id)
    {
        var game = minigames.Find(m => m.minigameID == id);
        return game != null && game.isCompleted;
    }

    public int GetReward()
    {
        return reward;
    }
}