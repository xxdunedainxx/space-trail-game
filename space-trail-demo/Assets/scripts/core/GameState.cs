using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState 
{
    public string currentLevelReference;
    public player playerReference;
    public static GameState instance { get; private set; }
    public LEVELS levelState = new LEVELS();

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

    public class LevelState
    {
        public bool completed { get; set; } = false;
    }

    public class LEVELS
    {
        public LevelState LECTURE_HALL = new LevelState();
        public LevelState HALLWAY = new LevelState();
    }
}
