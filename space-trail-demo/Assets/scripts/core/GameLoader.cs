using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;
using Assets.scripts.core.gameplay;
using Assets.scripts.core.dialogue;
using System;

namespace Assets.scripts.core
{
    public class GameLoader : MonoBehaviour
    {

        public static GameLoader instance { get; private set; }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        protected void Initialize()
        {
            Persistence.InitPersistence();
            this.InitializeDialogueManager();
            this.InitializeLevelTransitioner();
            this.InitLevelTransitioner();
            this.AddClickManager();
            this.AddGamePreferencesController();
        }

        protected void InitializeDialogueManager()
        {
            Debug.unityLogger.Log("level requires dialogue...");
            this.gameObject.AddComponent<DialogueWriter>();
            this.gameObject.AddComponent<DialogManager>();
        }


        protected void InitializeLevelTransitioner()
        {
            this.gameObject.AddComponent<LevelTransitionHandler>();
        }

        protected void InitializeCameraFollow()
        {
            this.gameObject.AddComponent<FollowPlayer>();
        }

        protected void AddClickManager()
        {
            Camera.main.gameObject.AddComponent<ClickManager>();
            ClickManager.instance.cam = Camera.main;
        }

        protected void InitLevelTransitioner()
        {
            this.gameObject.AddComponent<LevelLoader>();
        }

        protected void AddGamePreferencesController()
        {
            this.gameObject.AddComponent<GamePreferencesController>();
        }
    }

}
