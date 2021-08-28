using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core;
using Assets.scripts.core.gameplay;
using Assets.scripts.core.dialogue;
using System;

namespace Assets.scripts.LoadingScreens
{
    class Login : GameLoader
    {
        private readonly static string NewGameButtonName = "NewGameButton";
        private readonly static string LoadGameButtonName = "LoadGameButton";
        private readonly static string SettingsName = "SettingsButton";

        private Button NewGameButton;
        private Text NewGameText;


        private Button LoadGameButton;
        private Text LoadGameText;

        private Button SettingButton;
        private Text SettingText;

        private void Start()
        {
            Debug.unityLogger.Log("setting up login screen..");
            Persistence.playerRequired = false;
            this.Initialize();
            this.SetupButtons();
            this.AddButtonListeners();
        }

        private void SetupButtons()
        {
            GameObject nameButtonObj = GameObject.Find(Login.NewGameButtonName);
            GameObject loadGameObj = GameObject.Find(Login.LoadGameButtonName);
            GameObject settingObj = GameObject.Find(Login.SettingsName);


            this.NewGameButton = nameButtonObj.GetComponent<Button>();
            this.NewGameText = nameButtonObj.GetComponent<Text>();

            this.LoadGameButton = loadGameObj.GetComponent<Button>();
            this.LoadGameText = loadGameObj.GetComponent<Text>();

            this.SettingButton = settingObj.GetComponent<Button>();
            this.SettingText = settingObj.GetComponent<Text>();
        }

        private void AddButtonListeners()
        {
            Debug.unityLogger.Log("Adding button listeners");
            this.LoadGameButton.onClick.AddListener(this.LoadGame);
        }

        private void LoadGame()
        {
            Debug.unityLogger.Log("trying to load game..");
            LevelLoader.instance.levelTransition(GameState.getGameState().gsStore.LAST_LEVEL);
        }
    }
}
