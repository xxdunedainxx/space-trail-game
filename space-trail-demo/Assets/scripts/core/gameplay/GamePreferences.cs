using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Assets.scripts.core.GamePreferences;

namespace Assets.scripts.core
{
    [Serializable]
    public sealed class GamePreferences
    {
        [Serializable]
        public class UserInputs
        {

            public static KeyCode StringToKeyCode(string key)
            {
                return ((KeyCode)Enum.Parse(typeof(KeyCode), key.ToUpper()));
            }

            public  KeyCode moveDown = UserInputs.StringToKeyCode("S");
            public  KeyCode moveUp = UserInputs.StringToKeyCode("W");
            public  KeyCode moveLeft = UserInputs.StringToKeyCode("A");
            public  KeyCode moveRight = UserInputs.StringToKeyCode("D");
            public  KeyCode inventory = UserInputs.StringToKeyCode("I");
        }

        private static GamePreferences instance = null;
        public UserInputs inputs;

        private GamePreferences()
        {
            this.inputs = new UserInputs();
        }

        public static GamePreferences getPreferences()
        {
            if(GamePreferences.instance == null)
            {
                GamePreferences.instance = new GamePreferences();
            }
            return GamePreferences.instance;
        }

        public static void setGamePreferences(GamePreferences prefs)
        {
            GamePreferences.instance = prefs;
        }
    }

    public class GamePreferencesController : MonoBehaviour
    {
        public static GamePreferencesController instance;
        [SerializeField]
        public Dictionary<string, string> GameObjectLookupTable = new Dictionary<string, string>() {
            { "SettingsCanvas", "SettingsCanvas"},
            { "SettingsText", "SettingsText"},

            { "MovementSettingsToggle", "MovementSettingsToggle"},
            {"MovementSettingsToggleText", "MovementSettingsToggleText" },

            {"MoveDownSettings", "MoveDownSettings" },
            {"MoveDownSettingsText","MoveDownSettingsText" },
            {"MovedownDescription", "MovedownDescription" },

            {"MoveUpSettings", "MoveUpSettings" },
            {"MoveUpSettingsText","MoveUpSettingsText" },
            {"MoveUpDescription", "MoveUpDescription" },

            {"MoveRightSettings", "MoveRightSettings" },
            {"MoveRightSettingsText","MoveRightSettingsText" },
            {"MoveRightDescription", "MoveRightDescription" },

            {"MoveLeftSettings", "MoveLeftSettings" },
            {"MoveLeftSettingsText","MoveLeftSettingsText" },
            {"MoveLeftDescription", "MoveLeftDescription" },
            {"SettingsSaveButton","SettingsSaveButton" },

            {"SettingsSaveButtonText","SettingsSaveButtonText" }
        };

        private Canvas SettingsCanvas;

        private Text SettingsText;
        private Button MovementSettingsToggle;
        private Text MovementSettingsToggleText;

        private InputField MovedownSettingsField;
        private Text MovedownSettingsText;
        private Text MovedownDescription;

        private InputField MoveUpSettingsField;
        private Text MoveUpSettingsText;
        private Text MoveUpDescription;

        private InputField MoveRightSettingsField;
        private Text MoveRightSettingsText;
        private Text MoveRightDescription;

        private InputField MoveLeftSettingsField;
        private Text MoveLeftSettingsText;
        private Text MoveLeftDescription;

        private Button SaveButton;
        private Text SaveButtonText;

        private GamePreferences prefs;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                GamePreferencesController.instance = this;
            }
        }

        private void Start()
        {
            this.prefs = GamePreferences.getPreferences();
            this.GetAllUIObjects();
            this.AddOnClickListeners();
            this.DisableAll();
        }

        private void GetAllUIObjects()
        {
            this.SettingsCanvas = GameObject.Find(this.GameObjectLookupTable["SettingsCanvas"]).GetComponent<Canvas>();
            this.SettingsText = GameObject.Find(this.GameObjectLookupTable["SettingsText"]).GetComponent<Text>();

            this.GetMovementSettingsObjects();
            this.GetSaveObjects();
        }

        private void GetSaveObjects()
        {
            this.SaveButton = GameObject.Find(this.GameObjectLookupTable["SettingsSaveButton"]).GetComponent<Button>();
            this.SaveButtonText = GameObject.Find(this.GameObjectLookupTable["SettingsSaveButtonText"]).GetComponent<Text>();
        }

        private void GetMovementSettingsObjects()
        {

            this.MovementSettingsToggle = GameObject.Find(this.GameObjectLookupTable["MovementSettingsToggle"]).GetComponent<Button>();
            this.MovementSettingsToggleText = GameObject.Find(this.GameObjectLookupTable["MovementSettingsToggleText"]).GetComponent<Text>();

            this.MovedownSettingsField = GameObject.Find(this.GameObjectLookupTable["MoveDownSettings"]).GetComponent<InputField>();
            this.MovedownSettingsText = GameObject.Find(this.GameObjectLookupTable["MoveDownSettingsText"]).GetComponent<Text>();
            this.MovedownSettingsField.text = this.prefs.inputs.moveDown.ToString();
            this.MovedownDescription = GameObject.Find(this.GameObjectLookupTable["MovedownDescription"]).GetComponent<Text>();

            this.MoveUpSettingsField = GameObject.Find(this.GameObjectLookupTable["MoveUpSettings"]).GetComponent<InputField>();
            this.MoveUpSettingsText = GameObject.Find(this.GameObjectLookupTable["MoveUpSettingsText"]).GetComponent<Text>();
            this.MoveUpSettingsField.text = this.prefs.inputs.moveUp.ToString();
            this.MoveUpDescription = GameObject.Find(this.GameObjectLookupTable["MoveUpDescription"]).GetComponent<Text>();

            this.MoveRightSettingsField = GameObject.Find(this.GameObjectLookupTable["MoveRightSettings"]).GetComponent<InputField>();
            this.MoveRightSettingsText = GameObject.Find(this.GameObjectLookupTable["MoveRightSettingsText"]).GetComponent<Text>();
            this.MoveRightSettingsField.text = this.prefs.inputs.moveRight.ToString();
            this.MoveRightDescription = GameObject.Find(this.GameObjectLookupTable["MoveRightDescription"]).GetComponent<Text>();

            this.MoveLeftSettingsField = GameObject.Find(this.GameObjectLookupTable["MoveLeftSettings"]).GetComponent<InputField>();
            this.MoveLeftSettingsText = GameObject.Find(this.GameObjectLookupTable["MoveLeftSettingsText"]).GetComponent<Text>();
            this.MoveLeftSettingsField.text = this.prefs.inputs.moveLeft.ToString();
            this.MoveLeftDescription = GameObject.Find(this.GameObjectLookupTable["MoveLeftDescription"]).GetComponent<Text>();

        }

        private void AddOnClickListeners()
        {
            this.MovementSettingsToggle.onClick.AddListener(this.EnableMovementSettingsPage);
        }

        public void EnableSaveButton()
        {
            this.SaveButton.enabled = true;
            this.SaveButtonText.enabled = true;
        }

        public void DisableSaveButton()
        {
            this.SaveButton.enabled = false;
            this.SaveButtonText.enabled = false;
        }

        public void DisableAll()
        {
            this.SettingsCanvas.enabled = false;
            this.SettingsText.enabled = false;

            this.DisableMovementSettings();

            this.DisableSaveButton();
        }

        public void DisableMovementSettings()
        {
            this.MovementSettingsToggleText.enabled = false;
            this.MovementSettingsToggle.enabled = false;
            this.MovementSettingsToggle.image.enabled = false;

            this.MovedownSettingsField.enabled = false;
            this.MovedownSettingsField.image.enabled = false;
            this.MovedownSettingsText.enabled = false;
            this.MovedownDescription.enabled = false;


            this.MoveUpSettingsField.enabled = false;
            this.MoveUpSettingsField.image.enabled = false;
            this.MoveUpSettingsText.enabled = false;
            this.MoveUpDescription.enabled = false;


            this.MoveRightSettingsField.enabled = false;
            this.MoveRightSettingsField.image.enabled = false;
            this.MoveRightSettingsText.enabled = false;
            this.MoveRightDescription.enabled = false;


            this.MoveLeftSettingsField.enabled = false;
            this.MoveLeftSettingsField.image.enabled = false;
            this.MoveLeftSettingsText.enabled = false;
            this.MoveLeftDescription.enabled = false;
        }
    

        public void EnableSettingsMainPage()
        {
            this.SettingsCanvas.enabled = true;
            this.SettingsText.text = "Settings";
            this.SettingsText.enabled = true;

            this.MovementSettingsToggle.enabled = true;
            this.MovementSettingsToggle.image.enabled = true;
            this.MovementSettingsToggleText.enabled = true;
            this.EnableSaveButton();
        }

        public void DisableMainPage()
        {
            this.MovementSettingsToggle.enabled = false;
            this.MovementSettingsToggleText.enabled = false;
            this.MovementSettingsToggle.enabled = false;
            this.MovementSettingsToggle.image.enabled = false;
        }

        public void DisableMovementSettingsPage()
        {
            this.MovedownSettingsField.enabled = false;
            this.MovedownSettingsField.image.enabled = false;
            this.MovedownSettingsText.enabled = false;
            this.MovedownDescription.enabled = false;
            this.DisableMovementSettings();

        }

        public void EnableMovementSettingsPage()
        {
            this.DisableMainPage();
            this.SettingsText.text = "Movement Settings";

            this.MovedownSettingsField.enabled = true;
            this.MovedownSettingsText.enabled = true;
            this.MovedownSettingsField.image.enabled = true;
            this.MovedownDescription.enabled = true;

            this.MoveUpSettingsField.enabled = true;
            this.MoveUpSettingsField.image.enabled = true;
            this.MoveUpSettingsText.enabled = true;
            this.MoveUpDescription.enabled = true;


            this.MoveRightSettingsField.enabled = true;
            this.MoveRightSettingsField.image.enabled = true;
            this.MoveRightSettingsText.enabled = true;
            this.MoveRightDescription.enabled = true;


            this.MoveLeftSettingsField.enabled = true;
            this.MoveLeftSettingsField.image.enabled = true;
            this.MoveLeftSettingsText.enabled = true;
            this.MoveLeftDescription.enabled = true;

            this.SaveButton.onClick.RemoveAllListeners();
            this.SaveButton.onClick.AddListener(this.SaveMovementSettings);
        }

        private void SaveMovementSettings()
        {
            Debug.unityLogger.Log("trying to persist movement settings...");
            this.prefs.inputs.moveDown = UserInputs.StringToKeyCode(this.MovedownSettingsField.text);
            this.prefs.inputs.moveLeft = UserInputs.StringToKeyCode(this.MoveLeftSettingsField.text);
            this.prefs.inputs.moveRight = UserInputs.StringToKeyCode(this.MoveRightSettingsField.text);
            this.prefs.inputs.moveUp = UserInputs.StringToKeyCode(this.MoveUpSettingsField.text);
            Persistence.PersistData();
        }
    }
}
