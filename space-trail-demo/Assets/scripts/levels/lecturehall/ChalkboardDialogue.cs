using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.levels.lecturehall;
using Assets.scripts.core;
using Assets.scripts.core.gameplay;

public class ChalkboardDialogue : MonoBehaviour, IClickable
{
    private List<string> chalkBoardMessageDefault =  new List<string> { "Sigh.. There is some equation on the board that you do not understand.. We need to work on our studes :(" };
    private List<string> chalkBoardMessageLevelCompleted = new List<string> { "There is some equation on the board that you do not understand" };
    private Dialog chalkBoardDialogue;
    public LevelLectureHall lectureHallRef = null;
    private LayerMask interactLayer;

    public void click()
    {
        Debug.unityLogger.Log("chalk board clicked");
        if(lectureHallRef == null)
        {
            return;
        }

        if (CanInteract())
        {
            DialogManager manager = DialogManager.instance;
            if (lectureHallRef.levelComplete)
            {
                manager.StartDialogue(new Dialog(this.chalkBoardMessageLevelCompleted));
            }
            else
            {
                manager.StartDialogue(new Dialog(this.chalkBoardMessageDefault));
            }
        }
    }

    private void Awake()
    {
        this.interactLayer = Layers.PLAYER_LAYER;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanInteract()
    {
        Collider2D interactChecks = Physics2D.OverlapCircle(this.transform.position, 2f, interactLayer);
        if (interactChecks != null)
        {
            return true;
        }
        return false;
    }


}
