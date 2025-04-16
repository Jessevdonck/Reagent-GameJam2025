using UnityEngine;
using UnityEngine.UIElements;

public class DialogueInteractable : MonoBehaviour
{
    [Header("Dialogue Lines")]
    [TextArea(2, 5)] public string[] dialogueLines;

    [Header("Reward Lines")]
    [TextArea(2, 5)] public string[] rewardLines;

    [Header("Post-Reward Lines")]
    [TextArea(2, 5)] public string[] afterRewardLines;

    [Header("Character info")] 
    public string characterName;

    public Sprite characterPortrait;
    
    private bool rewardGiven = false;

    public void Interact()
    {
        if (DialogueUI.Instance.IsDialogueRunning) return;

        if (AllMinigamesCompleted())
        {
            if (!rewardGiven && rewardLines.Length > 0)
            {
                string rewardLine = rewardLines[Random.Range(0, rewardLines.Length)];
                DialogueUI.Instance.ShowDialogue(new string[] { rewardLine }, characterName, characterPortrait);
                rewardGiven = true;
                GameManager.Instance.AddMoney(100);
                Debug.Log("+100 euro!");
            }
            else if (afterRewardLines.Length > 0)
            {
                string postRewardLine = afterRewardLines[Random.Range(0, afterRewardLines.Length)];
                DialogueUI.Instance.ShowDialogue(new string[] { postRewardLine }, characterName, characterPortrait);
            }
        }
        else if (dialogueLines.Length > 0)
        {
            string randomLine = dialogueLines[Random.Range(0, dialogueLines.Length)];
            DialogueUI.Instance.ShowDialogue(new string[] { randomLine }, characterName, characterPortrait);
        }
    }

    private bool AllMinigamesCompleted()
    {
        // Placeholder tot je echte check implementeert
        return true;
    }
}