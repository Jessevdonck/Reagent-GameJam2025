using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public string speakerName;
    [TextArea(2, 5)]
    public List<string> sentences;
}