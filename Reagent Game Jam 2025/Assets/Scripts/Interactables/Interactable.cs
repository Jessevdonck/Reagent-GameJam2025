using MiniGame;
using Unity.Mathematics;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Minigame Settings")]
    public GameObject minigamePrefab;

    private GameObject activeMinigame;

    public virtual void Interact()
    {
        if (activeMinigame == null && minigamePrefab != null)
        {
            OpenMinigame();
        }
    }

    void OpenMinigame()
    {
        // Canvas canvas = FindObjectOfType<Canvas>();
        // if (canvas == null)
        // {
        //     Debug.LogError("Geen Canvas gevonden in de scene!");
        //     return;
        // }
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));

        activeMinigame = Instantiate(minigamePrefab, worldCenter,quaternion.identity);
        Debug.Log("pressed E3");
        // Interface check
        var minigame = activeMinigame.GetComponent<IMinigame>();
        if (minigame != null)
        {
            minigame.SetActivator(this);
        }
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