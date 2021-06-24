using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;

public class LevelBlocker : MonoBehaviour
{
    [SerializeField]
    public string output;
    [SerializeField]
    public LayerMask interactLayer;
    [SerializeField]
    public Transform body;
    [SerializeField]
    public Dialog dialog;

    private void Start()
    {
        this.dialog.sentences.Add(this.output);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Collider2D interactChecks = Physics2D.OverlapCircle(body.position, 0.5f, interactLayer);
        DialogManager manager = DialogManager.instance;
        manager.StartDialogue(this.dialog);
    }

}
