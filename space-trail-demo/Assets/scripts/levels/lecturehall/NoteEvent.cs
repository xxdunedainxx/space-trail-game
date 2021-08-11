using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.events;

namespace Assets.scripts.levels.lecturehall
{
    // disables the sparkle sprite and gives player the book + omeeds note
    public class NoteEvent : ObjectObtainedEvent
    {

        public BasicNote noteItem;
        private InvisibleBlock invisibleWall;
        private Dialog noteFoundDialogue = new Dialog(new List<string> { "Oh... it looks like you found my note in the book, i'll be taking that" });
        private EventLookupInfo diaologueEventLookup;

        public NoteEvent(BasicNote note, ref InvisibleBlock invisibleWall, EventLookupInfo dialogueEventLookup) : base()
        {
            this.noteItem = note;
            this.invisibleWall = invisibleWall;
            this.diaologueEventLookup = dialogueEventLookup;
        }

        public override void execute()
        {
            Debug.unityLogger.Log($"Executing note event? {this.active()}");

            DialogManager.instance.StartDialogue(this.noteFoundDialogue);
            player p = GameState.getGameState().playerReference;
            p.removeFromInventory(this.noteItem.name());
            Debug.unityLogger.Log($"Setting invisible wall to background image {Layers.BACKGROUND_IMAGE_LAYER_VALUE}");
            this.invisibleWall.Blocking = false;
            GameState.getGameState().levelState.LECTURE_HALL.completed = true;

            this.setEventInactive();

            Dialog newOmeedMessage = new Dialog(new List<string>{ "Thanks again.." });
            DialogueEvent ev = (DialogueEvent)EventSubscriptionFactory.instance.GetEvent(this.diaologueEventLookup);
            ev.OverrideDialogue(newOmeedMessage);

            EventSubscriptionFactory.instance.eventEnableQueue.Enqueue(this.diaologueEventLookup);
        }
    }
}
