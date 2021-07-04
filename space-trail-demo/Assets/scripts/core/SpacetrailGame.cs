using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;
using Assets.scripts.core.gameplay;
using Assets.scripts.core.dialogue;
using System;

public class SpacetrailGame : MonoBehaviour
{
    [SerializeField]
    string level;
    Level lvl;

    private void getLevel()
    {
        Debug.unityLogger.Log($"Spacetrailgame is getting level {level}");
        this.lvl = LevelFactory.FetchLevel(this.level);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.getLevel();
        Persistence.InitPersistence();
        this.lvl.gameState = GameState.getGameState();
        this.lvl.gameState.currentLevelReference = lvl.name;
        this.InitGameState();
        this.InitGamePreferences();
        StartCoroutine(this.WaitForEvents(this.doLast));
        this.InitializeDialogueManager();


    }

    private void doLast()
    {
        Debug.unityLogger.Log("Co-routines complete, doing last stuff");
        this.InitPlayerState();
        this.lvl.startLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitForEvents(Action doLast)
    {
        yield return StartCoroutine(WaitForPlayer());
        doLast();
    }


    IEnumerator WaitForPlayer()
    {
        Debug.unityLogger.Log("waiting for player");
        yield return new WaitUntil(this.playerReady);
        Debug.unityLogger.Log("player is ready!");
    }

    bool playerReady()
    {
        return this.lvl.gameState.playerReference != null;
    }

    private void InitGameState()
    {
        GameState.getGameState().levelState = Persistence.persistenceLayer.levelState;
    }

    private void InitGamePreferences()
    {
        GamePreferences.setGamePreferences(Persistence.persistenceLayer.prefs);
    }

    private void InitPlayerState()
    {

        player p = GameState.getGameState().playerReference;
        p.initPlayer();
        p.setPlayerState(Persistence.persistenceLayer.player);
    }

    private void InitializeDialogueManager()
    {
        if(this.lvl.requiresDialogue)
        {
            Debug.unityLogger.Log("level requires dialogue...");
            this.gameObject.AddComponent<DialogueWriter>();
            this.gameObject.AddComponent<DialogManager>();
        }
    }
}
