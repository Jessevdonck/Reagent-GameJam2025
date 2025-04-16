using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;
    
    [Header("Character Display")]
    public TMP_Text nameText;
    public UnityEngine.UI.Image portraitImage;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    private Queue<string> sentences = new Queue<string>();
    private System.Action onComplete;

    public bool IsDialogueRunning => dialoguePanel.activeSelf;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string[] lines, string characterName = "", Sprite characterPortrait = null, System.Action onDialogueComplete = null)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }

        nameText.text = characterName;
        portraitImage.sprite = characterPortrait;
        portraitImage.gameObject.SetActive(characterPortrait != null);

        onComplete = onDialogueComplete;
        dialoguePanel.SetActive(true);
        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        onComplete?.Invoke();
    }
}