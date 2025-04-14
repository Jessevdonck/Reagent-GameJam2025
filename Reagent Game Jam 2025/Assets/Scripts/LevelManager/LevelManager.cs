using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private LevelData[] levels;
    private int currentLevelIndex = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    public void GoalReached()
    {
        Debug.Log("Goal reached!");
        currentLevelIndex++;

        if (currentLevelIndex < levels.Length)
            LoadLevel(currentLevelIndex);
        else
            Debug.Log("All levels complete!");
    }

    private void LoadLevel(int index)
    {
        Debug.Log("Loading Level " + index);
        ClearLevel();

        Instantiate(levels[index].levelPrefab);
    }

    private void ClearLevel()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Level"))
        {
            Destroy(obj);
        }
    }
}