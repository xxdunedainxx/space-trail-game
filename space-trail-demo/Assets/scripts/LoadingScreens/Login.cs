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
        private readonly static string LoginCanvas = "LoginCanvas";
        private readonly static string NewGameButtonName = "NewGameButton";
        private readonly static string NewGameButtonTextName = "NewGameButtonText";
        private readonly static string LoadGameButtonName = "LoadGameButton";
        private readonly static string LoadGameButtonTextName = "LoadGameButtonText";
        private readonly static string SettingsName = "SettingsButton";
        private readonly static string SettingsTextName = "SettingsButtonText";

        private Canvas LoginCanvasObject;

        private Button NewGameButton;
        private Text NewGameText;


        private Button LoadGameButton;
        private Text LoadGameText;

        private Button SettingButton;
        private Text SettingText;

        private GamePreferencesController gamePrefs;

        private void Start()
        {
            Debug.unityLogger.Log("setting up login screen..");
            Persistence.playerRequired = false;
            this.Initialize();
            this.SetupButtons();
            this.AddButtonListeners();
            this.gamePrefs = this.gameObject.GetComponent<GamePreferencesController>();
            this.CheckIfGameSaveExists();
        }

        private void DisableLoadButton()
        {
            this.LoadGameButton.enabled = false;
            this.LoadGameText.enabled = false;
        }

        private void CheckIfGameSaveExists()
        {
            if(GameState.getGameState().gsStore.LAST_LEVEL == "")
            {
                Debug.unityLogger.Log("no save file dected, removing load widget...");
                this.DisableLoadButton();
            }
        }

        private void NewGame()
        {
            Level.levelTransition(LevelFactory.LECTURE_HALL);
        }

        private void SetupButtons()
        {
            GameObject LoginCanvasO = GameObject.Find(Login.LoginCanvas);

            GameObject nameButtonObj = GameObject.Find(Login.NewGameButtonName);
            GameObject nameButtonTextObj = GameObject.Find(Login.NewGameButtonTextName);

            GameObject loadGameObj = GameObject.Find(Login.LoadGameButtonName);
            GameObject loadGameObjText = GameObject.Find(Login.LoadGameButtonTextName);

            GameObject settingObj = GameObject.Find(Login.SettingsName);
            GameObject settingObjText = GameObject.Find(Login.SettingsTextName);

            this.LoginCanvasObject = LoginCanvasO.GetComponent<Canvas>();

            this.NewGameButton = nameButtonObj.GetComponent<Button>();
            this.NewGameText = nameButtonTextObj.GetComponent<Text>();

            this.LoadGameButton = loadGameObj.GetComponent<Button>();
            this.LoadGameText = loadGameObjText.GetComponent<Text>();

            this.SettingButton = settingObj.GetComponent<Button>();
            this.SettingText = settingObjText.GetComponent<Text>();
        }

        private void AddButtonListeners()
        {
            Debug.unityLogger.Log("Adding button listeners");
            this.LoadGameButton.onClick.AddListener(this.LoadGame);
            this.SettingButton.onClick.AddListener(this.Settings);
            this.NewGameButton.onClick.AddListener(this.NewGame);
        }

        private void LoadGame()
        {
            Debug.unityLogger.Log("trying to load game..");
            LevelLoader.instance.levelTransition(GameState.getGameState().gsStore.LAST_LEVEL);
        }

        private void Settings()
        {
            this.UnloadMainPage();
            this.gamePrefs.EnableSettingsMainPage();
        }

        private void UnloadMainPage()
        {
            this.LoginCanvasObject.enabled = false;

            this.NewGameButton.enabled = false;
            this.NewGameText.enabled = false;

            this.LoadGameButton.enabled = false;
            this.LoadGameText.enabled = false;

            this.SettingButton.enabled = false;
            this.SettingText.enabled = false;
        }

        private void MainPage()
        {
            this.LoginCanvasObject.enabled = true;
            this.NewGameButton.enabled = true;
            this.NewGameText.enabled = true;

            this.LoadGameButton.enabled = true;
            this.LoadGameText.enabled = true;

            this.SettingButton.enabled = true;
            this.SettingText.enabled = true;
        }
    }
}
