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
        public bool StickWithPlayer = false;

        public void Update()
        {
            if(this.StickWithPlayer)
            {
                this.AdjustToPlayerPosition();
            }
        }

        private void AdjustToPlayerPosition()
        {
            Vector3 playerPos = this.transform.position = GameState.getGameState().playerReference.gameObject.transform.position;

            this.transform.position = new Vector3(playerPos.x + .15f, playerPos.y, playerPos.z);
        }

        public void DefaultPosture()
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/shared/old-lady-v1");
        }


        public void DontStayWithPlayer()
        {
            this.StickWithPlayer = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        public void StayWithPlayer()
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.StickWithPlayer = true;
            this.DisableShakeAnimation();
            this.DefaultPosture();
        }

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

        public void ShakeAnimation()
        {
            Debug.unityLogger.Log("Old lady  shake animation");
            Animator anim = this.GetComponent<Animator>();
            anim.enabled = true;
            anim.Play("old-lady-shakey-anim", -1, 0f);
        }

        public void DisableShakeAnimation()
        {
            Animator anim = this.GetComponent<Animator>();
            anim.enabled = false;
        }
    }
    public class CrossStreetGameEvent : IEvent
    {
        static readonly string eventName = "crossStreatGame";
        private Dialog startGameDialogue = new Dialog(new List<string>() { "Hey you! YAH YOU! You look like a nice young strapping young lad... Would you mind helping me across the street?" });
        private bool enabled = true;
        public OldLady oldLadyRef;
        private float originalPlayerSpeed = 1f;

        public bool active()
        {
            return this.enabled;
        }

        public void SetOldLadyReference(ref OldLady lady)
        {
            this.oldLadyRef = lady;
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
            this.originalPlayerSpeed = GameState.getGameState().playerReference.getPlayerState().movementSpeed;
            GameState.getGameState().playerReference.getPlayerState().movementSpeed = this.originalPlayerSpeed / 4;
            this.oldLadyRef.StayWithPlayer();
            GameObject.Find("crossStreetGameEnd").AddComponent<CrossStreetGameEndListener>();
            CrossStreetGameEvent g = this;
            GameObject.Find("crossStreetGameEnd").GetComponent<CrossStreetGameEndListener>().SetGameRef(ref g);
        }

        public void EndGame()
        {
            GameState.getGameState().playerReference.getPlayerState().movementSpeed = this.originalPlayerSpeed;
            this.oldLadyRef.DontStayWithPlayer();
            this.setEventInactive();
            GameState.getGameState().levelState.CITY.eventToggles["crossStreet"] = false;
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

        public class CrossStreetGameEndListener : MonoBehaviour {
            private readonly Dialog endGameDialogue = new Dialog(new List<string> { "Thank you so much for helping a little old lady like me... I will repay you somehow!" });
            public CrossStreetGameEvent eventRef;

            public void OnTriggerEnter2D(Collider2D collider)
            {
                UnityEngine.Debug.unityLogger.Log($"CrossStreetGame {collider.gameObject.layer}, vs {Layers.PLAYER_LAYER_VALUE}");
                Debug.unityLogger.Log("on collision enter!!");
                if(collider.gameObject.layer == Layers.PLAYER_LAYER_VALUE)
                {
                    Debug.unityLogger.Log("they reached the end!!");
                    DialogManager.instance.StartDialogue(this.endGameDialogue);
                    this.eventRef.EndGame();
                    Destroy(this.gameObject);
                }
            }

            public void SetGameRef(ref CrossStreetGameEvent ev) {
                this.eventRef = ev;
            }
        }
    }
}
