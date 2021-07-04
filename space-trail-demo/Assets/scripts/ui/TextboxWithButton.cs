using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextboxWithButton : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, string> GameObjectLookupTable = new Dictionary<string, string>() {
        {"Text", "CanvasDialogueTextBox" },
        {"Button", "CanvasDialogueButton" },
        {"ButtonText","CanvasDialogueButtonText" },
        {"ButtonImage", "CanvasDialogueBackgroundImage" }
    };

    public Text textBox;
    public Button button;
    public Text buttonText;
    public Image backgroundImage;

    private void Awake()
    {
        Debug.unityLogger.Log("TEXTBOXWITHBUTTON AWAKE?");
        Debug.unityLogger.Log("SETTING UP TEXTBOX MANAGER OBJECT");
        this.textBox = GameObject.Find(GameObjectLookupTable["Text"]).GetComponent<Text>();
        this.textBox.gameObject.AddComponent<ContentSizeFitter>();
        ContentSizeFitter c = this.textBox.GetComponent<ContentSizeFitter>();
        this.textBox.fontSize = 25;
        c.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        c.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        this.button = GameObject.Find(GameObjectLookupTable["Button"]).GetComponent<Button>();
        this.button.onClick.AddListener(this.clickButton);
        this.buttonText = GameObject.Find(GameObjectLookupTable["ButtonText"]).GetComponent<Text>();
        this.backgroundImage = GameObject.Find(GameObjectLookupTable["ButtonImage"]).GetComponent<Image>();
        StartCoroutine(WaitForManager());
    }

    private bool DialogManagerIsReady()
    {
        Debug.unityLogger.Log("Waiting for DialogManager instance...");
        return DialogManager.instance != null;
    }

    private void SetManagerTextboxRef()
    {
        Debug.unityLogger.Log("setting text box ref...");
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

    public TextboxWithButton()
    {

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
        Debug.unityLogger.Log("disable text box with button stuff");
        this.textBox.text = ""; //clear text before disabling
        this.textBox.enabled = false;
        this.button.enabled = false;
        this.button.image.enabled = false;
        this.buttonText.enabled = false;
        this.backgroundImage.enabled = false;
    }
}
