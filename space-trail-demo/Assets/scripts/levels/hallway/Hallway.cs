using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.gameplay;
using Assets.scripts.levels.lecturehall;
using Assets.scripts.core.constants;

public class Hallway : Level
{
    private Omeed omed;
    private Dialog omeedDialogue = new Dialog(new List<string> { "Hmmm looks like a bunch of new jobs were just posted..." });

    private readonly string MARS_FLYER = "mars-adventure-flyer";
    private readonly Dialog marsDialogue = new Dialog(new List<string> { "Mars terraform project info!", "uhhhh" });
    private DecisionFlyer marsFlyer;

    private readonly string SPACE_CREW_FLYER = "space-crew-adventure-flyer";
    private readonly Dialog spaceCrewDialogue = new Dialog(new List<string> { "Space crew project info!", "uhhhh" });
    private DecisionFlyer spaceCrewFlyer;

    private readonly string CORP_FLYER = "new-beginnings-corporate-flyer";
    private readonly Dialog corpDialogue = new Dialog(new List<string> { "Corporate story line..", "uhhhh" });
    private DecisionFlyer corpFlyer;

    public Hallway() : base("Hallway", false)
    {
        this.transitionHandlers = new Dictionary<string, Vector3>
        {
            {LevelFactory.TA_OFFICE,  new Vector3(0.775f, -0.181f, 0)},
            {LevelFactory.OUTSIDE_LECTUREHALL,  new Vector3(1.551f, -0.711f, 0)}
        };
    }

    public override void startLevel() {
        base.startLevel();

        Debug.unityLogger.Log("hallway constructor");
        if (GameState.getGameState().levelState.HALLWAY.eventToggles["firstTimeLoad"] == true)
        {
            this.omed = GameObject.Find("omeed").GetComponent<Omeed>();
            GameObject.Find("omeed").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("omeed").GetComponent<BoxCollider2D>().enabled = true;
            this.omed.dialog = this.omeedDialogue;
            this.MoveOmeed();
            GameState.getGameState().levelState.HALLWAY.eventToggles["firstTimeLoad"] = false;
        }
        else
        {
            GameObject.Find("omeed").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("omeed").GetComponent<BoxCollider2D>().enabled = false;
        }
        this.AddFlyerListeners();
    }

    private void AddFlyerListeners()
    {
        GameObject.Find(this.MARS_FLYER).AddComponent<DecisionFlyer>();
        this.marsFlyer = GameObject.Find(this.MARS_FLYER).GetComponent<DecisionFlyer>();
        this.marsFlyer.storyLineName = ConstStrings.StoryLineStrings.MARS_STORY_LINE;
        this.marsFlyer.clickDialog = this.marsDialogue;


        GameObject.Find(this.SPACE_CREW_FLYER).AddComponent<DecisionFlyer>();
        this.spaceCrewFlyer = GameObject.Find(this.SPACE_CREW_FLYER).GetComponent<DecisionFlyer>();
        this.spaceCrewFlyer.storyLineName = ConstStrings.StoryLineStrings.SPACE_CREW_STORY_LINE;
        this.spaceCrewFlyer.clickDialog = this.spaceCrewDialogue;

        GameObject.Find(this.CORP_FLYER).AddComponent<DecisionFlyer>();
        this.corpFlyer = GameObject.Find(this.CORP_FLYER).GetComponent<DecisionFlyer>();
        this.corpFlyer.storyLineName = ConstStrings.StoryLineStrings.CORPORATE_STORY_LINE;
        this.corpFlyer.clickDialog = this.corpDialogue;
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
        Debug.unityLogger.Log($"Last level: {GameState.getGameState().gsStore.LAST_LEVEL}");
        string lastLevel = GameState.getGameState().gsStore.LAST_LEVEL;
        Debug.unityLogger.Log($"Last level was {lastLevel}, adjusting player position");
        this.transitionHandler();
    }
    

}