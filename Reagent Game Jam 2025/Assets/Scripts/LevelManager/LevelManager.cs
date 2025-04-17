using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public List<MinigameData> minigames;

    private void Awake()
    {
        Instance = this;
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
}
