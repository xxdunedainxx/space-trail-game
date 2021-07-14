using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameState 
{
    public string currentLevelReference;
    public player playerReference = null;
    public static GameState instance { get; private set; }
    public LEVELS levelState = new LEVELS();
    private bool ready = false;
    public string LAST_LEVEL = "";

    public GameState()
    {
        Debug.unityLogger.Log("GameState Instantiated");
    }

    public static GameState getGameState()
    {
        if (GameState.instance == null)
        {
            GameState.instance = new GameState();
        }
        return GameState.instance;
    }

    public static void setGameState(GameState nState)
    {
        GameState.instance = nState;
    }

    public bool gameStateReady()
    {
        return this.ready;
    }

    public void setReady()
    {
        this.ready = true;
    }

    public void notReady()
    {
        this.ready = false;
    }

    [Serializable]
    public class LevelState
    {
        public bool completed { get; set; } = false;
    }

    [Serializable]
    public class LEVELS
    {
        public LevelState LECTURE_HALL = new LevelState();
        public LevelState HALLWAY = new LevelState();
    }
}
