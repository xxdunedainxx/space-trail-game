using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc : MonoBehaviour, IClickable
{
    public Dialog dialog;
    public Transform body;
    public TextBoxWithButton textBox;
    public LayerMask interactLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerDialog()
    {

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

    public void click()
    {
        if (this.textBox != null && this.dialog != null)
        {
            Debug.unityLogger.Log("NPC was clicked!");
            if (CanInteract())
            {
                Debug.unityLogger.Log($"User is close enough for interaction");
                DialogManager manager = DialogManager.getManager(this.textBox);
                
                manager.StartDialogue(this.dialog);
            }
            else
            {
                Debug.unityLogger.Log("USer is not close enough for interaction..");
            }
        }
    }
}
