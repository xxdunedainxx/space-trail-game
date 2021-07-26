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
    private static Vector3 taOfficeStartingPoint = new Vector3(0.775f, -0.181f, 0);

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
            GameState.getGameState().playerReference.adjustPlayerPosition(Hallway.taOfficeStartingPoint);
        }
    }
    

}