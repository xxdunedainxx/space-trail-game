using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class DialogManager : MonoBehaviour
{

    private Queue<string> sentences;
    public static DialogManager instance { get; private set; }
    private static readonly object padlock = new object();
    public TextBoxWithButton textBoxReference;

    private DialogManager()
    {
        sentences = new Queue<string>();
    }

    private void Awake()
    {
        lock (padlock)
        {
            if (DialogManager.instance == null)
            {
                Debug.unityLogger.Log($"Starting dialog manager with text reference");
                DialogManager.instance = new DialogManager();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


    public void StartDialogue(Dialog dialogue) {
        this.sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        this.textBoxReference.enable();
        this.DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            this.EndDialogue();
        }
        else
        {
            string sentence = this.sentences.Dequeue();
            this.textBoxReference.textBox.text = sentence;
        }
    }

    private void EndDialogue()
    {
        Debug.unityLogger.Log("finished dilgoue");
        this.textBoxReference.hide();
    }
}
