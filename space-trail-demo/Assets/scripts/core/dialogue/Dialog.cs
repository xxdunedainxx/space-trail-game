using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{ 
    [TextArea(3,10)]
    public List<string> sentences;

    public Dialog(List<string> s)
    {
        this.sentences = s;
    }
}
