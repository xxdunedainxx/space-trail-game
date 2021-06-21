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

    public TextboxWithButton(Text textb, Button btn, Text btnText = null)
    {
        this.textBox = textb;
        this.button = btn;
        this.buttonText = btnText;
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
    }

    public void disable()
    {
        this.textBox.text = ""; //clear text before disabling
        this.textBox.enabled = false;
        this.button.enabled = false;
        this.button.image.enabled = false;
        this.buttonText.enabled = false;
    }
}
