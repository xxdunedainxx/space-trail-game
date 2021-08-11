using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core.dialogue;
using System;

public sealed class DialogManager : MonoBehaviour
{

    private Queue<string> sentences;
    public static DialogManager instance { get; private set; }
    private static readonly object padlock = new object();
    public DialogueWriter writer;
    public TextboxWithButton textBoxReference = null;
    public YesNoButtons yesNoBtns = null;
    public Canvas canvasRef = null;
    public Dialog currentDialogue = null;
    public AdvancedDialogue currentAdvancedDialogue = null;
    private Action endDialogueCallBack = null;
    private bool finishedLastSentence = false;

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
            this.gameObject.AddComponent<YesNoButtons>();
            this.yesNoBtns = this.gameObject.GetComponent<YesNoButtons>();
            this.textBoxReference = this.gameObject.GetComponent<TextboxWithButton>();
            this.writer = DialogueWriter.instance;
            this.canvasRef = GameObject.Find("CanvasDialogue").GetComponent<Canvas>();
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

    public void PrepareDialogue(Action endDialogueCallback, bool yesNoButtonsEnabled)
    {
        this.EndDialogue();
        this.endDialogueCallBack = endDialogueCallback;
        this.textBoxReference.enable();


        if (yesNoButtonsEnabled)
        {
            this.yesNoBtns.enable();
        }

    }

    public void StartDialogue(AdvancedDialogue dialogue, Action endDialogueCallback = null, bool yesNoButtonsEnabled = false) {
        this.PrepareDialogue(endDialogueCallback, yesNoButtonsEnabled);
        this.currentAdvancedDialogue = dialogue;
        this.DisplayNextSentence();
    }

    public void StartDialogue(Dialog dialogue, Action endDialogueCallback = null, bool yesNoButtonsEnabled = false) {
        this.PrepareDialogue(endDialogueCallback, yesNoButtonsEnabled);

        this.currentDialogue = dialogue;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        this.DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(this.currentDialogue != null)
        {
            this.PrintRegularDialogue();
        } else if(this.currentAdvancedDialogue != null)
        {
            this.PrintAdvancedDialogue();
        }
        else
        {
            Debug.unityLogger.Log("ooo no dialogues to print :(");
        }

    }
    
    public void PrintAdvancedDialogue()
    {
        if (this.writer.IsWriting())
        {
            this.writer.SetToEnd();
        }
        else if (! this.currentAdvancedDialogue.RunNextDialogue())
        {
            this.CheckWriterBeforeEnd();
        }
    }

    private void CheckWriterBeforeEnd()
    {
        if (this.writer.IsWriting())
        {
            this.writer.SetToEnd();
        }
        else
        {
            this.EndDialogue();
        }
    }

    public void PrintRegularDialogue()
    {
        if (sentences.Count <= 0)
        {
            this.CheckWriterBeforeEnd();
        }
        else
        {
            string sentence = this.sentences.Dequeue();
            if (this.currentDialogue.dialogueTime == 0)
            {
                this.PrintSentence(sentence);
            }
            else
            {
                this.PrintSentence(sentence, this.currentDialogue.dialogueTime);
            }
        }
    }

    public void PrintSentence(string sentence, float printTime)
    {
        if (this.writer.AutoCompletedLast())
        {
            this.writer.UnsetAutoComplete();
        }

        if (this.writer.IsWriting())
        {
            this.writer.SetToEnd();
        }
        else
        {
            this.writer.PrintSentence(sentence, printTime);
        }
    }

    public void PrintSentence(string sentence) {
       this.textBoxReference.textBox.text = sentence;
    }

    public void EndDialogue()
    {
        Debug.unityLogger.Log("finished dilgoue");
        this.currentDialogue = null;
        this.sentences.Clear();
        this.writer.UnsetVars();
        this.textBoxReference.disable();
        this.yesNoBtns.disable();
        if(this.endDialogueCallBack != null)
        {
            Debug.unityLogger.Log("calling end dialogue callback");
            this.endDialogueCallBack();
        }
    }

}
