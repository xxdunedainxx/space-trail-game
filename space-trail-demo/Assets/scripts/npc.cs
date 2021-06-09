using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc : MonoBehaviour, IClickable
{
    [SerializeField]
    public Dialog dialog;
    [SerializeField]
    public Transform body;
    [SerializeField]
    public string name;
    [SerializeField]
    public LayerMask interactLayer;
    [SerializeField]
    List<string> dynamicSentences = null;

    public void Awake()
    {
        if(this.dynamicSentences!= null)
        {
            this.generateDynamicSentences();
        }

        if(this.dialog.sentences.Count == 0)
        {
            this.dialog.sentences = new List<string> {"..."};
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

    public void click()
    {
        if (this.dialog != null)
        {
            Debug.unityLogger.Log("NPC was clicked!");
            if (CanInteract())
            {
                Debug.unityLogger.Log("User is close enough for interaction");
                DialogManager manager = DialogManager.instance;
                manager.StartDialogue(this.dialog);
            }
            else
            {
                Debug.unityLogger.Log("USer is not close enough for interaction..");
            }
        }
    }

    private void generateDynamicSentences()
    {
        List<string> newSentencesList = new List<string>();

        for(int i = 0; i < this.dynamicSentences.Count; i++)
        {
            if (this.dynamicSentences[i].Contains("${name}"))
            {
                this.dynamicSentences[i] = this.dynamicSentences[i].Replace("${name}", this.name);
            }
        }
        newSentencesList.AddRange(this.dynamicSentences);
        newSentencesList.AddRange(this.dialog.sentences);

        this.dialog.sentences = newSentencesList;
    }
}
