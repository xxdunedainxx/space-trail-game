using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core
{
    [Serializable]
    public sealed class GamePreferences
    {
        [Serializable]
        public class UserInputs
        {
            public  KeyCode moveDown = KeyCode.S;
            public  KeyCode moveUp = KeyCode.W;
            public  KeyCode moveLeft = KeyCode.A;
            public  KeyCode moveRight = KeyCode.D;
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
}
