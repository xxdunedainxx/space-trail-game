using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core;

public class UIInventoryItem : MonoBehaviour
{

    public BasicItem associatedItem = null;
    public GameObject inventoryObject = null;
    public Button inventoryObjectButton = null;
    public Sprite inventorySprite = null;
    public Image originalInvImage = null;
    private Dialog defaultDialogue = new Dialog(new List<string> { "An empty item slot.. :( " });

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (associatedItem != null && inventoryObject != null)
        {

        }
    }

    void UpdateInventorySprite()
    {

    }

    public void PrintItemInfo()
    {
        Debug.unityLogger.Log("printing item info..");
        DialogManager manager = DialogManager.instance;
        if(this.associatedItem != null)
        {
            Dialog itemDialogue = new Dialog(new List<string> { $"Item Description: {this.associatedItem.description()}. Name: {this.associatedItem.name()}" });
            manager.StartDialogue(itemDialogue);
        }
        else
        {
            manager.StartDialogue(this.defaultDialogue);
        }
        
    }

    public void Enable()
    {
        this.inventoryObjectButton.enabled = true;
        this.originalInvImage.enabled = true;
    }

    public void Disable()
    {
        this.inventoryObjectButton.enabled = false;
        this.originalInvImage.enabled = false;

    }
}
