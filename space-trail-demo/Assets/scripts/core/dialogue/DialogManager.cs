using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core.dialogue;

public sealed class DialogManager : MonoBehaviour
{

    private Queue<string> sentences;
    public static DialogManager instance { get; private set; }
    private static readonly object padlock = new object();
    public DialogueWriter writer;
    public TextboxWithButton textBoxReference = null;
    public Dialog currentDialogue;

    public DialogManager()
    {
        sentences = new Queue<string>();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.unityLogger.Log("setting up objects for dialogue manager");
            this.gameObject.AddComponent<TextboxWithButton>();
            this.textBoxReference = this.gameObject.GetComponent<TextboxWithButton>();
            this.gameObject.AddComponent<DialogueWriter>();
            this.writer = this.gameObject.GetComponent<DialogueWriter>();
            SetInstance();
            StartCoroutine(this.WaitForTextBoxSet());
        }
    }

    private void Start()
    {
        
    }

    bool TextBoxReady()
    {
        Debug.unityLogger.Log("waiting for text box...");
        return this.textBoxReference != null;
    }

    private void SetInstance()
    {
        instance = this;
    }

    private void FinishTextWriter()
    {
        this.writer.textBox = this.textBoxReference.textBox;
    }

    IEnumerator WaitForTextBoxSet()
    {
        yield return new WaitUntil(TextBoxReady);
        Debug.unityLogger.Log("Text box ready!");
        this.SetInstance();
        this.FinishTextWriter();
    }


    public void StartDialogue(Dialog dialogue) {
        this.sentences.Clear();
        this.currentDialogue = dialogue;
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Debug.unityLogger.Log($"starting dialogue {dialogue.sentences}");
        this.textBoxReference.enable();
        this.DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (this.currentDialogue.dialogueTime == 0)
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
        else
        {

            if (this.writer.IsWriting())
            {
                this.writer.SetToEnd();
            }
            else
            {
                if (sentences.Count <= 0)
                {
                    this.EndDialogue();
                }
                else
                {
                    string sentence = this.sentences.Dequeue();
                    this.writer.PrintSentence(sentence, this.currentDialogue.dialogueTime);
                }
            }
        }
    }

    private void EndDialogue()
    {
        Debug.unityLogger.Log("finished dilgoue");
        this.textBoxReference.disable();
    }

}
