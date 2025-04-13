using UnityEngine;
using System.Collections.Generic;
using InventorySystem;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelPrefabs;
    [SerializeField] private Transform levelParent;
    [SerializeField] private CameraController camController;

    private int currentLevelIndex = -1;
    private GameObject currentLevel;

    void Start()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levelPrefabs.Count)
        {
            Debug.Log("All levels completed!");
            return;
        }

        if (currentLevel != null)
            Destroy(currentLevel);

        currentLevel = Instantiate(levelPrefabs[currentLevelIndex], levelParent);

        // Camera uitzoomen als het nodig is
        camController.FocusOn(currentLevel.transform, zoomOut: ShouldZoom(currentLevelIndex));

        // Welke componenten moeten unlocken
        UnlockComponentsForLevel(currentLevelIndex);
    }

    private bool ShouldZoom(int index)
    {
        // Welke levels moeten uitzoomen
        return (index == 1 || index == 3);
    }

    private void UnlockComponentsForLevel(int index)
    {
        // OM TE TESTEN, later is dit via data bestandje ofzo
        var inv = FindObjectOfType<Inventory>();
        if (index == 1)
        {
            inv.addResistor(5);
        }
        else if (index == 2)
        {
            inv.addSplitter(3);
        }
    }
}