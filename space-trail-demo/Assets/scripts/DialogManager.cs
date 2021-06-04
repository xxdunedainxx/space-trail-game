using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class DialogManager : MonoBehaviour
{

    private Queue<string> sentences;
    private static DialogManager instance = null;
    private static readonly object padlock = new object();
    public Text textBoxReference;

    public DialogManager(Text textBox)
    {
        sentences = new Queue<string>();
        textBoxReference = textBox;
    }

    public static DialogManager getManager(Text textBox = null)
    {
        lock(padlock)
        {
            if(DialogManager.instance == null)
            {
                DialogManager.instance = new DialogManager(textBox);
            }
            return DialogManager.instance;
        }
    }
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }


    public void StartDialogue(Dialog dialogue) {
        Debug.unityLogger.Log($"Starting convo with {dialogue.npcName} | {this.textBoxReference}");

        this.sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        this.textBoxReference.enabled = true;
        this.DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count <= 0)
        {
            this.EndDialogue();
        }
        string sentence = this.sentences.Dequeue();
        this.textBoxReference.text = sentence;
    }

    private void EndDialogue()
    {
        Debug.unityLogger.Log("finished dilgoue");
        this.textBoxReference.enabled = false;

    }

}
