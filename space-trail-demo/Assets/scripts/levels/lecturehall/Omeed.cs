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
        public NoteEvent nEvent;
        private Dialog noteFoundDialogue = new Dialog(new List<string> { "Oh... it looks like you found my note in the book, i'll be taking that" });

        public override void click()
        {
            Debug.unityLogger.Log("Omeed was clicked?");
            if (CanInteract())
            {
                this.orientImage();
                if (this.nEvent.active())
                {
                    Debug.unityLogger.Log("Note event is active");
                    player p = GameState.getGameState().playerReference;
                    this.nEvent.execute();
                    this.interact(this.noteFoundDialogue);
                    this.nEvent.setEventInactive();
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