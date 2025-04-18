using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class DialogueInteractable : MonoBehaviour
{
    [Header("First-time Dialogue Lines")]
    [TextArea(2, 5)] public string[] firstTimeDialogueLines; 
    
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
    private bool firstTimeTalking = true;

    public GameObject exclamation;

    public void Start()
    {
        if (firstTimeTalking)
        {
            exclamation.SetActive(true);
        }
        else
        {
            exclamation.SetActive(false);
        }
    }

    public void Interact()
    {
        if (DialogueUI.Instance.IsDialogueRunning) return;

        if (LevelManager.Instance.AreAllMinigamesCompleted())
        {
            if (!rewardGiven)
            {
                LevelManager.Instance.StopTimer();
                string rewardLine = rewardLines[Random.Range(0, rewardLines.Length)];
                DialogueUI.Instance.ShowDialogue(new string[] { rewardLine }, characterName, characterPortrait);
                rewardGiven = true;
                GameManager.Instance.AddMoney(LevelManager.Instance.GetReward());
                
            }
            else
            {
                string postRewardLine = afterRewardLines[Random.Range(0, afterRewardLines.Length)];
                DialogueUI.Instance.ShowDialogue(new string[] { postRewardLine }, characterName, characterPortrait);
            }
        }
        else
        {
            if (firstTimeTalking)
            {
                string firstTimeLine = firstTimeDialogueLines[Random.Range(0, firstTimeDialogueLines.Length)];
                DialogueUI.Instance.ShowDialogue(new string[] { firstTimeLine }, characterName, characterPortrait);
                firstTimeTalking = false;
                
                exclamation.SetActive(false);
                
                LevelManager.Instance.ShowMinigameExclamations();
                
                LevelManager.Instance.StartTimer();
            }
            else
            {
                string randomLine = dialogueLines[Random.Range(0, dialogueLines.Length)];
                DialogueUI.Instance.ShowDialogue(new string[] { randomLine }, characterName, characterPortrait);
            }
        }
    }
}