using MiniGame;
using Unity.Mathematics;
using UnityEngine;

public class MinigameInteractable : MonoBehaviour, IInteractable
{
    [Header("Minigame Settings")]
    public GameObject minigamePrefab;

    private GameObject activeMinigame;

    public void Interact()
    {
        if (activeMinigame == null && minigamePrefab != null)
        {
            OpenMinigame();
        }
    }

    void OpenMinigame()
    {
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));

        activeMinigame = Instantiate(minigamePrefab, worldCenter,quaternion.identity);
        
    }

    public void DestroyAllButtons()
    {
        activeMinigame.GetComponent<CounterGame>().destroyAllButtons();
    }

    public void CloseMinigame()
    {
        if (activeMinigame != null)
        {
            DestroyAllButtons();
            Destroy(activeMinigame);
            activeMinigame = null;
        }
    }
}