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
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Geen Canvas gevonden in de scene!");
            return;
        }

        activeMinigame = Instantiate(minigamePrefab, canvas.transform);

        // Interface check
        var minigame = activeMinigame.GetComponent<IMinigame>();
        if (minigame != null)
        {
            minigame.SetActivator(this);
        }
    }

    public void CloseMinigame()
    {
        if (activeMinigame != null)
        {
            Destroy(activeMinigame);
            activeMinigame = null;
        }
    }
}