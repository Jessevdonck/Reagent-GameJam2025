using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/Create New Level")]
public class LevelData : ScriptableObject
{
    public GameObject levelPrefab;
}