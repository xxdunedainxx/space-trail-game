using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{ 
    [TextArea(3,10)]
    public List<string> sentences;
    public float dialogueTime = 0;

    public Dialog(List<string> s, int dialogueTime = 0)
    {
        this.sentences = s;
        this.dialogueTime = dialogueTime;
    }
}
