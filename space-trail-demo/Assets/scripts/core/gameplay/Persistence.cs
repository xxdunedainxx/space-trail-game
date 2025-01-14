﻿using System;
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
        public static bool playerRequired = true;
        public static PersistenceLayer persistenceLayer = null;
        // https://www.newtonsoft.com/json/help/html/SerializingJSON.htm
        public static void InitPersistence()
        {
            Debug.unityLogger.Log("Persistence layer");
            if (!File.Exists(persistencePath))
            {
                Persistence.PersistData(true);
            }
            using (Stream stream = File.Open(Persistence.persistencePath, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                Persistence.persistenceLayer = (PersistenceLayer)bformatter.Deserialize(stream);
            }
            Persistence.initialized = true;
            Persistence.InitGameState();
            Persistence.InitGamePreferences();
            if (Persistence.playerRequired)
            {
                Persistence.InitPlayerState();
            }
            Persistence.persistenceLayer.printState();
        }

        public static bool CheckInitialized()
        {
            return Persistence.initialized;
        }

        public static void PersistData(bool firstTime = false)
        {
            if (persistenceLayer == null)
            {
                persistenceLayer = new PersistenceLayer();
            }

            if (!firstTime)
            {
                persistenceLayer.initDefaultObjects();
            }
            else
            {
                persistenceLayer.firstTimeLoad();
            }
            using (Stream stream = File.Open(Persistence.persistencePath, FileMode.OpenOrCreate))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, persistenceLayer);
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
            GameState.getGameState().gsStore = persistenceLayer.gameStateStore;
        }

        private static void InitPlayerState()
        {
            
            player p = GameState.getGameState().playerReference;
            p.initPlayer();
            p.setPlayerState(persistenceLayer.player);
        }

        private static void waitForPlayer()
        {
            while(GameState.getGameState().playerReference == null)
            {
                System.Threading.Thread.Sleep(5000);
                Debug.unityLogger.Log("waiting for player object");
            }
        }
    }

    [Serializable]
    class PersistenceLayer
    {
        public GameState.LEVELS levelState;
        public GameState.GameStateStore gameStateStore;
        public GamePreferences prefs;
        public PlayerState player;

        public PersistenceLayer(GameState.LEVELS levelState, GamePreferences prefs, PlayerState p, GameState.GameStateStore gs)
        {
            this.levelState = levelState;
            this.prefs = prefs;
            this.player = p;
            this.gameStateStore = gs;
        }

        public PersistenceLayer()
        {

        }

        public void firstTimeLoad()
        {
            this.levelState = GameState.getGameState().levelState;
            this.gameStateStore = GameState.getGameState().gsStore;
            this.prefs = GamePreferences.getPreferences();
            this.player = new PlayerState();
        }

        public void initDefaultObjects()
        {
            this.levelState = GameState.getGameState().levelState;
            this.gameStateStore = GameState.getGameState().gsStore;
            this.prefs = GamePreferences.getPreferences();
            if (Persistence.playerRequired)
            {
                this.player = GameState.getGameState().playerReference.getPlayerState();
            }
        }

        public void printState()
        {
            this.player.printPlayerState();
            this.gameStateStore.PrintGameStateStore();
        }
    }
}
