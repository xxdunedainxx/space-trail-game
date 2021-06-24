using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    protected GameState gameState;
    protected EventTree eventTree;
    public string name;
    public bool isVerticalLevel;
    
    public Level(string name, bool isVerticalLevel = false)
    {
        Debug.unityLogger.Log("Level Instantiated");
        Persistence.InitPersistence();
        this.gameState = GameState.getGameState();
        this.name = name;
        this.gameState.currentLevelReference = this.name;
        this.isVerticalLevel = isVerticalLevel;
    }

    protected void Start()
    {
        if (isVerticalLevel)
        {
            Debug.unityLogger.Log("applying vertical graviy");
            this.applyVerticalGravity();
        }
    }

    public virtual void constructEventTree()
    {
        Debug.unityLogger.Log("Level construct event tree called");
    }
 
    public static void levelTransition(string toLevel)
    {
        Persistence.PersistData();
        SceneManager.LoadScene(toLevel);
    }

    private void applyVerticalGravity()
    {
        player p = GameState.getGameState().playerReference;
        p.gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
        p.sideScrolling = true;
    }
}
