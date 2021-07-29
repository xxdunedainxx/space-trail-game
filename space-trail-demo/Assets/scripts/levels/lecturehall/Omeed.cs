using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts;
using Assets.scripts.core;

namespace Assets.scripts.levels.lecturehall
{
    public class Omeed : npc, IClickable
    {
        public static string THE_BOOK_OMEEDS_NOTE_IS_IN = "Intro to metereology";

        public override void click()
        {
            Debug.unityLogger.Log("Omeed was clicked?");
            if (CanInteract())
            {
                this.orientImage();
                if (this.events != null || this.eventLookups != null)
                {
                    Debug.unityLogger.Log("Executing events??");
                    this.ExecuteEventListeners();
                }
                else
                {
                   this.interact(this.dialog);
                }
            }
        }

        private void interact(Dialog d)
        {
            DialogManager manager = DialogManager.instance;
            GameState.getGameState().playerReference.removeFromInventory(LevelLectureHall.OMEEDS_NOTE);
            manager.StartDialogue(d);
        }
    }
}