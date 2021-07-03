using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextboxWithButton : MonoBehaviour
{
    [SerializeField]
    public Text textBox;
    [SerializeField]
    public Button button;
    [SerializeField]
    public Text buttonText;
    [SerializeField]
    public Image backgroundImage;

    private void Awake()
    {
        StartCoroutine(WaitForManager());
    }

    private bool DialogManagerIsReady()
    {
        Debug.unityLogger.Log("Waiting for DialogManager instance...");
        return DialogManager.instance != null;
    }

    private void SetManagerTextboxRef()
    {
        DialogManager manager = DialogManager.instance;
        manager.textBoxReference = this;
    }

    IEnumerator WaitForManager()
    {
        yield return new WaitUntil(DialogManagerIsReady);
        SetManagerTextboxRef();

    }


    public TextboxWithButton(Text textb, Button btn, Text btnText = null, Image backgroundImage = null)
    {
        this.textBox = textb;
        this.button = btn;
        this.buttonText = btnText;
        this.backgroundImage = backgroundImage;
    }

    public void clickButton()
    {
        DialogManager dialogue = DialogManager.instance;
        dialogue.DisplayNextSentence();
    }

    public void enable()
    {
        this.textBox.enabled = true;
        this.button.enabled = true;
        this.button.image.enabled = true;
        this.buttonText.enabled = true;
        this.backgroundImage.enabled = true;
    }

    public void disable()
    {
        this.textBox.text = ""; //clear text before disabling
        this.textBox.enabled = false;
        this.button.enabled = false;
        this.button.image.enabled = false;
        this.buttonText.enabled = false;
        this.backgroundImage.enabled = false;
    }
}
