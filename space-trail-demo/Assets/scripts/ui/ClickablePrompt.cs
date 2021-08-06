using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;

public class ClickablePrompt : MonoBehaviour, IClickable
{
    [SerializeField]
    public string name = "some object";
    [SerializeField]
    public Transform body;
    private LayerMask interactLayer;
    [SerializeField]
    public Dialog message = new Dialog(new List<string> { "Something?" });

    void Awake()
    {
        this.interactLayer = Layers.PLAYER_LAYER;
        this.body = this.gameObject.transform;
    }

    public void click()
    {
        Debug.unityLogger.Log($"{name} was clicked!");
        if (CanInteract())
        {
            Debug.unityLogger.Log("close enough to interact..");
            DialogManager manager = DialogManager.instance;
            manager.StartDialogue(this.message);
                
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
