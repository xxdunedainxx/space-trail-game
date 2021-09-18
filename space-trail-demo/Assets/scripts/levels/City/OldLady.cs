using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts;
using Assets.scripts.core;
using Assets.scripts.core.events;

namespace Assets.scripts.levels
{
    public class OldLady: npc, IClickable
    {
        public override void click()
        {
            Debug.unityLogger.Log("Old lady was clicked!");
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
            manager.StartDialogue(d);
        }
    }
    public class CrossStreetGameEvent : IEvent
    {
        static readonly string eventName = "crossStreatGame";
        private Dialog startGameDialogue = new Dialog(new List<string>() { "Hey you! YAH YOU! You look like a nice young strapping young lad... Would you mind helping me across the street?" });
        private bool enabled = true;

        public bool active()
        {
            return this.enabled;
        }

        public List<EventLookupInfo> contingentEvents()
        {
            return null;
        }

        public void execute()
        {
            this.RequestStartGame();
        }

        public string name()
        {
            return CrossStreetGameEvent.eventName;
        }

        public void setEventActive()
        {
            this.enabled = true;
        }

        public void setEventInactive()
        {
            this.enabled = false;
        }

        private void StartGame()
        {
            DialogManager.instance.EndDialogue();
        }

        private void NoGame()
        {
            DialogManager.instance.PrintSentence("well fine!");
        }

        void RequestStartGame()
        {
            DialogManager mgr = DialogManager.instance;
            mgr.yesNoBtns.yesButton.onClick.AddListener(this.StartGame);
            mgr.yesNoBtns.noButton.onClick.AddListener(this.NoGame);
            mgr.StartDialogue(this.startGameDialogue, yesNoButtonsEnabled: true);
        }
    }
}
