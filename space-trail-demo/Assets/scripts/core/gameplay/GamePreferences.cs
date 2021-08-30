using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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
                return ((KeyCode)Enum.Parse(typeof(KeyCode), key));
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
            {"MoveDownSettings", "MoveDownSettings" }
        };

        private Canvas SettingsCanvas;

        private Text SettingsText;
        private Button MovementSettingsToggle;
        private Text MovementSettingsToggleText;
        private InputField MovedownSettingsField;

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
            this.MovementSettingsToggle = GameObject.Find(this.GameObjectLookupTable["MovementSettingsToggle"]).GetComponent<Button>();
            this.MovementSettingsToggleText = GameObject.Find(this.GameObjectLookupTable["MovementSettingsToggleText"]).GetComponent<Text>();
            this.MovedownSettingsField = GameObject.Find(this.GameObjectLookupTable["MoveDownSettings"]).GetComponent<InputField>();
            this.MovedownSettingsField.text = this.prefs.inputs.moveDown.ToString();
        }

        private void AddOnClickListeners()
        {
            this.MovementSettingsToggle.onClick.AddListener(this.EnableMovementSettingsPage);
        }

        public void DisableAll()
        {
            this.SettingsCanvas.enabled = false;
            this.SettingsText.enabled = false;
            this.MovedownSettingsField.enabled = false;
            this.MovementSettingsToggleText.enabled = false;
            this.MovedownSettingsField.enabled = false;
        }

        public void EnableSettingsMainPage()
        {
            this.SettingsCanvas.enabled = true;
            this.SettingsText.enabled = true;
            this.MovementSettingsToggle.enabled = true;
            this.MovementSettingsToggleText.enabled = true;
        }

        public void DisableMainPage()
        {
            this.SettingsText.enabled = false;
            this.MovementSettingsToggle.enabled = false;
            this.MovementSettingsToggleText.enabled = false;
        }

        public void DisableMovementSettingsPage()
        {
            this.MovedownSettingsField.enabled = false;
        }

        public void EnableMovementSettingsPage()
        {
            this.DisableMainPage();
            this.MovedownSettingsField.enabled = true;
        }
    }
}
