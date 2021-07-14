using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core;

public class Door : MonoBehaviour, IClickable
{
    [SerializeField]
    public string name;
    [SerializeField]
    public Transform body;
    public LayerMask interactLayer;
    [SerializeField]
    string sceneToTransitionTo = "NONE";
    public Dialog noLevelDialogue = new Dialog(new List<string> {"It appears the door is locked.."});

    public void Awake()
    {
        this.interactLayer = Layers.PLAYER_LAYER;
        if(this.body == null)
        {
            this.body = this.gameObject.transform;
        }
    }

    public void click()
    {
        Debug.unityLogger.Log("door was clicked!");
        if (CanInteract())
        {
            Debug.unityLogger.Log("close enough to interact..");
            if(this.sceneToTransitionTo != "NONE")
            {
                Level.levelTransition(sceneToTransitionTo);
            }
            else
            {
                DialogManager manager = DialogManager.instance;
                Debug.unityLogger.Log(manager);
                manager.StartDialogue(this.noLevelDialogue);
            }
        }
        else
        {
            Debug.unityLogger.Log("not close enough to itnract with door");
        }
    }

    

    public bool CanInteract()
    {
        Collider2D interactChecks = Physics2D.OverlapCircle(body.position, 0.5f, interactLayer);
        if (interactChecks != null)
        {
            return true;
        }
        return false;
    }

}
