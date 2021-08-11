using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.core.events;

namespace Assets.scripts.core.objects
{
    public class DialogueEvent : IEvent
    {

        private List<string> sentences;
        protected Dialog dialog;
        private string eventName;
        private bool isActive;
        List<EventLookupInfo> dependentEvents;


        public DialogueEvent(List<string> sentences,string name, float dialogueTime = 0)
        {
            this.sentences = sentences;
            this.dialog = new Dialog(sentences, dialogueTime);
            this.eventName = name;
        }

        public virtual string name()
        {
            return $"{this.eventName}-event";
        }

        public virtual void setEventInactive()
        {
            this.isActive = false;
        }

        public virtual void setEventActive()
        {
            this.isActive = true;
        }

        public virtual bool active()
        {
            return this.isActive;
        }

        public virtual List<EventLookupInfo> contingentEvents()
        {
            return this.dependentEvents;
        }

        public virtual void execute()
        {
            DialogManager manager = DialogManager.instance;
            manager.StartDialogue(this.dialog);
        }

        public void OverrideDialogue(Dialog d)
        {
            this.dialog = d;
        }
    }
}
