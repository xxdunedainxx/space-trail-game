using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;
using Assets.scripts.core.objects;

public class DecisionFlyer : MonoBehaviour, IClickable
{

    public Dialog clickDialog;
    public string storyLineName;

    public bool CanInteract()
    {
        return true;
    }

    public void click()
    {
        DialogManager mgr = DialogManager.instance;
        mgr.yesNoBtns.yesButton.onClick.RemoveAllListeners();
        mgr.yesNoBtns.yesButton.onClick.AddListener(this.YesSelected);
        mgr.StartDialogue(this.clickDialog, yesNoButtonsEnabled: true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AreYouSure()
    {
        DialogManager mgr = DialogManager.instance;
        mgr.yesNoBtns.yesButton.onClick.RemoveAllListeners();
        mgr.yesNoBtns.yesButton.onClick.AddListener(this.AreYouSurePromptYes);
        Dialog areYouSurePrompt = new Dialog(new List<string>() { $"Are you sure you want to override '{GameState.getGameState().STORY_LINE_CHOSEN}'?" });
        mgr.StartDialogue(areYouSurePrompt, yesNoButtonsEnabled: true);
    }

    void AreYouSurePromptYes()
    {
        GameState.getGameState().STORY_LINE_CHOSEN = this.storyLineName;
        DialogManager.instance.EndDialogue();
    }

    void YesSelected()
    {
        Debug.unityLogger.Log($"User chose {this.storyLineName}");
        if (GameState.getGameState().STORY_LINE_CHOSEN == "")
        {
            GameState.getGameState().STORY_LINE_CHOSEN = this.storyLineName;
            DialogManager.instance.EndDialogue();
        }
        else
        {
            // ask are you sure you want to override?
            this.AreYouSure();
        }
        
    }
}
