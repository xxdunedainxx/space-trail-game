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
    public static SpacetrailGame instance { get; private set; }

    private void getLevel()
    {
        Debug.unityLogger.Log($"Spacetrailgame is getting level {level}");
        this.lvl = LevelFactory.FetchLevel(this.level);
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
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
        this.InitializeLevelTransitioner();
        this.InitLevelTransitioner();
        this.AddClickManager();
    }

    private void doLast()
    {
        Debug.unityLogger.Log("Co-routines complete, doing last stuff");
        this.InitPlayerState();
        this.lvl.startLevel();
        this.InitializeCameraFollow();
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

    private void InitLevelTransitioner()
    {
        this.gameObject.AddComponent<LevelLoader>();
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


    private void InitializeLevelTransitioner()
    {
        this.gameObject.AddComponent<LevelTransitionHandler>();
    }

    private void InitializeCameraFollow()
    {
        this.gameObject.AddComponent<FollowPlayer>();
    }

    private void AddClickManager()
    {
        Camera.main.gameObject.AddComponent<ClickManager>();
        ClickManager.instance.cam = Camera.main;
    }
}
