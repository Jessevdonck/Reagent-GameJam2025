using UnityEngine;

public class FakeGoal : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Simulated goal complete.");
            LevelManager.Instance.GoalReached();
        }
    }
}