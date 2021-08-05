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

    void YesSelected()
    {
        Debug.unityLogger.Log($"User chose {this.storyLineName}");
        if (GameState.getGameState().STORY_LINE_CHOSEN == "")
        {

        }
        else
        {
            GameState.getGameState().STORY_LINE_CHOSEN = this.storyLineName;
        }
        DialogManager.instance.EndDialogue();
    }
}
