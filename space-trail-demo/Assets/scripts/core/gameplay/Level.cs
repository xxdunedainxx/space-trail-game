using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;
using UnityEngine.SceneManagement;
using System.Threading;

public class Level 
{
    public GameState gameState;
    public string name;
    public bool isVerticalLevel;
    public bool requiresDialogue;

    public Level(string name, bool isVerticalLevel = false, bool dialogue = true)
    {
        Debug.unityLogger.Log($"'{name}' level Instantiated");

        this.name = name;
        this.isVerticalLevel = isVerticalLevel;
        this.requiresDialogue = dialogue;
    }

    public virtual void prepareLevel()
    {
        Debug.unityLogger.Log("empty preperation... nothing to do");

    }

    public virtual void constructEventTree()
    {
        Debug.unityLogger.Log("Level construct event tree called");
    }

    public virtual void startLevel()
    {
        this.prepareLevel();
        Debug.unityLogger.Log("empty start level..");

        if (isVerticalLevel)
        {
            Debug.unityLogger.Log("applying vertical graviy");
            this.applyVerticalGravity();
        }
        GameState.getGameState().LAST_LEVEL = this.name;
    }
 
    public static void levelTransition(string toLevel)
    {
        LevelLoader.instance.levelTransition(toLevel);
    }

    private void applyVerticalGravity()
    {
        player p = GameState.getGameState().playerReference;
        p.gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
        p.getPlayerState().movementSpeed = 5;
        p.sideScrolling = true;
    }

}
