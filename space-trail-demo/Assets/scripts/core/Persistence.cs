using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core
{
    class Persistence
    {
        private static bool initialized = false;
        private static string persistencePath = "game_data.json";
        private static PersistenceLayer persistenceLayer;
        // https://www.newtonsoft.com/json/help/html/SerializingJSON.htm
        public static void InitPersistence()
        {
            Debug.unityLogger.Log("Persistence layer");
            if (!File.Exists(persistencePath))
            {
                Persistence.PersistData();
            }
            else
            {
                Persistence.persistenceLayer = JsonConvert.DeserializeObject<PersistenceLayer>(File.ReadAllText(Persistence.persistencePath));
                Persistence.initialized = true;
                Persistence.InitGameState();
                Persistence.InitGamePreferences();
            }
            
        }

        private static bool CheckInitialized()
        {
            return Persistence.initialized;
        }

        public static void PersistData()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(persistencePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                PersistenceLayer p = new PersistenceLayer();
                p.initDefaultObjects();
                serializer.Serialize(writer, p);
            }
        }

        private static void InitGamePreferences()
        {
            if (!CheckInitialized())
            {
                return;
            }
            GamePreferences.setGamePreferences(persistenceLayer.prefs);
        }

        private static void InitGameState()
        {
            if (!CheckInitialized())
            {
                return;
            }
            GameState.getGameState().levelState = persistenceLayer.levelState;
        }
    }


    class PersistenceLayer
    {
        public GameState.LEVELS levelState;
        public GamePreferences prefs;

        public PersistenceLayer(GameState.LEVELS levelState, GamePreferences prefs)
        {
            this.levelState = levelState;
            this.prefs = prefs;
        }

        public PersistenceLayer()
        {

        }

        public void initDefaultObjects()
        {
            this.levelState = GameState.getGameState().levelState;
            this.prefs = GamePreferences.getPreferences();
        }
    }
}
