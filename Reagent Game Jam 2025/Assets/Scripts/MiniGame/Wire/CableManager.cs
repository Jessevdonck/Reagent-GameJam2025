using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour, IMinigame
{
    [SerializeField] private List<CableConnector> topRow;
    [SerializeField] private List<CableConnector> bottomRow;
    private bool gameCompleted;
    private int totalPairs;
    public MinigameInteractable interactableParent;
    [SerializeField] private Transform wireParent;
    private void Start()
    {
        gameCompleted = false;
        SetupWires();
    }

    void SetupWires()
    {
        if (topRow.Count != bottomRow.Count)
        {
            Debug.LogError("Connector lists must be the same length.");
            return;
        }

        totalPairs = topRow.Count;

        // Shuffle right connectors
        List<CableConnector> shuffledRight = new List<CableConnector>(bottomRow);
        for (int i = 0; i < shuffledRight.Count; i++)
        {
            CableConnector temp = shuffledRight[i];
            int randIndex = Random.Range(i, shuffledRight.Count);
            shuffledRight[i] = shuffledRight[randIndex];
            shuffledRight[randIndex] = temp;
        }

        // Assign correct pairs
        for (int i = 0; i < totalPairs; i++)
        {
            topRow[i].SetCorrect(shuffledRight[i]);
            topRow[i].SetWireParent(wireParent);
    
            shuffledRight[i].SetCorrect(topRow[i]);
            shuffledRight[i].SetWireParent(wireParent);
        }
    }

    public void CheckWinCondition()
    {
        foreach (var connector in topRow)
        {
            if (!connector.IsCorrectlyConnected())
                return;
        }

        Debug.Log("🎉 All cables correctly connected!");
        gameCompleted = true;
        
        LevelManager.Instance.MarkMinigameComplete(3); 

        if (interactableParent != null)
        {
            interactableParent.OnMinigameCompleted();
        }
        SelfDestruct();
        StartCoroutine(EndMinigame());
    }

    public bool GetGameCompleted()
    {
        return gameCompleted;
    }
    
    public void SetParentInteractable(MinigameInteractable interactable)
    {
        interactableParent = interactable;
    }

    public void SelfDestruct()
    {
        foreach (CableConnector connector in topRow)
        {
            connector.destroyWire();
            
        }
        foreach (CableConnector connector in bottomRow)
        {
            connector.destroyWire();
            
        }
    }

    private IEnumerator EndMinigame()
    {
        yield return new WaitForSeconds(2f);
        StopAllCoroutines();
        Destroy(this.gameObject);
    }
}
