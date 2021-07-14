using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts.core;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.objects;
using Assets.scripts.core.gameplay;

public class Hallway : Level
{
    public Hallway() : base("Hallway", false)
    {
        Debug.unityLogger.Log("hallway constructor");
    }

    public override void prepareLevel()
    {
        Debug.unityLogger.Log($"Last level: {GameState.getGameState().LAST_LEVEL}");
        if(GameState.getGameState().LAST_LEVEL == LevelFactory.TA_OFFICE)
        {
            Debug.unityLogger.Log("Last level was TA Office, adjusting player position");
        }
    }
    

}