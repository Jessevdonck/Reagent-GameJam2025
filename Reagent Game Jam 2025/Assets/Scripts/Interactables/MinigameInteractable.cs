using System;
using MiniGame;
using Unity.Mathematics;
using UnityEngine;

public class MinigameInteractable : MonoBehaviour, IInteractable
{
    
    [Header("Minigame Settings")]
    public GameObject minigamePrefab;
    
    [SerializeField] private int minigameID;
    [SerializeField] private GameObject exclamation;

    private bool isCompleted;
    private GameObject activeMinigame;
    
    public void Interact()
    {
        if (activeMinigame == null && minigamePrefab != null && !isCompleted)
        {
            OpenMinigame();
        }
    }

    void OpenMinigame()
    {
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));

        activeMinigame = Instantiate(minigamePrefab, worldCenter,quaternion.identity);
        IMinigame minigame = activeMinigame.GetComponent<IMinigame>();
        if (minigame != null)
        {
            minigame.SetParentInteractable(this);
        }
    }
    
    public void DisableInteractable()
    {
        gameObject.SetActive(false);
    }
    
    // public void DestroyAllButtons()
    // {
    //     activeMinigame.GetComponent<CounterGame>().destroyAllButtons();
    // }

    public void CloseMinigame()
    {
        if (activeMinigame != null)
        {
            Destroy(activeMinigame);
            activeMinigame = null;

            if (LevelManager.Instance.IsMinigameCompleted(minigameID))
            {
                gameObject.SetActive(false);
            }

            // if (activeMinigame.GetComponent<CounterGame>())
            // {
            //     DestroyAllButtons();
            // }
        }
    }
    
    public void OnMinigameCompleted()
    {
        isCompleted = true;
        exclamation.SetActive(false); // ⛔️ zet het uitroepteken uit
        CloseMinigame();              // sluit de minigame
    }
}