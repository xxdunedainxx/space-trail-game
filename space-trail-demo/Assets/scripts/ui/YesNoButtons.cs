using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesNoButtons : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, string> GameObjectLookupTable = new Dictionary<string, string>() {
        {"YesButton", "YesButton" },
        {"YesButtonText", "YesButtonText" },
        {"NoButton","NoButton" },
        {"NoButtonText", "NoButtonText" },
    };

    public Text yesButtonText;
    public Button yesButton;
    public Text noButtonText;
    public Button noButton;

    // Start is called before the first frame update
    void Start()
    {
        this.yesButtonText = GameObject.Find(GameObjectLookupTable["YesButtonText"]).GetComponent<Text>();
        this.yesButton = GameObject.Find(GameObjectLookupTable["YesButton"]).GetComponent<Button>();
        this.noButtonText = GameObject.Find(GameObjectLookupTable["NoButtonText"]).GetComponent<Text>();
        this.noButton = GameObject.Find(GameObjectLookupTable["NoButton"]).GetComponent<Button>();
        this.disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disable()
    {
        Debug.unityLogger.Log("disable yes no buttons with button stuff");
        this.yesButtonText.enabled = false;
        this.yesButton.enabled = false;
        this.yesButton.image.enabled = false;

        this.noButtonText.enabled = false;
        this.noButton.enabled = false;
        this.noButton.image.enabled = false;
    }

    public void enable()
    {
        this.yesButtonText.enabled = true;
        this.yesButton.enabled = true;
        this.yesButton.image.enabled = true;

        this.noButtonText.enabled = true;
        this.noButton.enabled = true;
        this.noButton.image.enabled = true;
    }
}
