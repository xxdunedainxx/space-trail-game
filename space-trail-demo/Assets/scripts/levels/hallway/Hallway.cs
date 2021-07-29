using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts.core;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.objects;
using Assets.scripts.core.gameplay;
using Assets.scripts.levels.lecturehall;

public class Hallway : Level
{
    private static Vector3 taOfficeStartingPoint = new Vector3(0.775f, -0.181f, 0);
    private Omeed omed;
    public Hallway() : base("Hallway", false)
    {

    }

    public override void startLevel() {
        base.startLevel();

        Debug.unityLogger.Log("hallway constructor");
        if (GameState.getGameState().levelState.HALLWAY.eventToggles["firstTimeLoad"] == true)
        {
            this.omed = GameObject.Find("omeed").GetComponent<Omeed>();
            GameObject.Find("omeed").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("omeed").GetComponent<BoxCollider2D>().enabled = true;
            this.MoveOmeed();
            GameState.getGameState().levelState.HALLWAY.eventToggles["firstTimeLoad"] = false;
        }
        else
        {
            GameObject.Find("omeed").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("omeed").GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void MoveOmeed()
    {
        this.MoveOmeedEast();
    }

    public void MoveOmeedEast()
    {
        this.omed.Move(.5f, 1, new Vector2(.25f, 0), endMovementCallBack: this.MoveOmeedWest, 2);
    }

    public void MoveOmeedWest()
    {
        this.omed.Move(.5f, 1, new Vector2(-.25f, 0), this.MoveOmeedEast, 2);
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