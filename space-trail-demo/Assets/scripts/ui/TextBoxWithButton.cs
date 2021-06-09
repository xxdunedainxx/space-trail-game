using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxWithButton : MonoBehaviour
{
    [SerializeField]
    public Text textBox;
    [SerializeField]
    public Text buttonText;
    [SerializeField]
    public Button button;
    

    public TextBoxWithButton(Text text, Button button, Text buttonText = null) {
        Debug.unityLogger.Log("Init textboxwithbutton");

        this.textBox = text;
        this.button = button;
        this.buttonText = buttonText;

        // enable both by default until triggered
        this.hide();
    }
    
    public void clickButton()
    {
        Debug.unityLogger.Log("TextBoxWithButton Button WAS CLICKED!");
        DialogManager manager = DialogManager.instance;
        manager.DisplayNextSentence();
    }

    public void enable()
    {
        this.textBox.enabled = true;
        this.button.enabled = true;
        this.button.image.enabled = true;
        if (this.buttonText) { this.buttonText.enabled = true; }
    }

    public void hide()
    {
        Debug.unityLogger.Log("disabling text box..");
        this.textBox.enabled = false;
        this.button.enabled = false;
        this.button.image.enabled = false;
        if (this.buttonText) { this.buttonText.enabled = false; };
    }

}
