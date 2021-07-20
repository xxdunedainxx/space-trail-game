using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Assets.scripts.core;
using Assets.scripts.core.objects;
//https://www.youtube.com/watch?v=wkKsl1Mfp5M <--- good ex of projectiles 

public class PaperTossGame : MonoBehaviour
{
    private bool holdingDownKey = false;
    private float _force = 0f;
    private float baseForce = 20f;
    private GameObject _paper;
    private Stopwatch sw;
    private const float startingPosition = .5f;
    private Dialog startGameDialogue = new Dialog(new List<string>() { "Hey kid, if you can 3/5 paper tosses in that trash can, i'll give you something cool" });
    private Dialog startGameD = new Dialog(new List<string>() { "Start game!" });
    private bool dialogueStarted = false;
    private bool gameStarted = false;
    private PaperTossSuccessEvent _event;
    public int successfullTosses = 0;
    public int attempts = 0;

    private int totalAllowedAttempts = 5;
    private int requiredAttempts = 3;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.unityLogger.Log("Paper toss game loaded!");
        this._paper = GameObject.Find("paper-toss");
        this._event = new PaperTossSuccessEvent(new LabKit("TA's Lab Kit", new List<LabKitComponent> { LabKit.MICROSCOPE }));
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                UnityEngine.Debug.unityLogger.Log("Loading up paper toss..");
                this.sw = new Stopwatch();
                this.sw.Start();
            }
            else if (Input.GetKeyUp(KeyCode.T))
            {
                player p = GameState.getGameState().playerReference;
                this.sw.Stop();

                UnityEngine.Debug.unityLogger.Log("they tossed?");
                GameObject obj = Instantiate(this._paper);
                obj.GetComponent<Paper>().primaryCopy = false;
                obj.GetComponent<Paper>().parentGame = this;
                obj.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                this.GenerateVector(ref obj);
                this.holdingDownKey = false;
            }
            this.CheckGame();
        }
        else
        {
            this.CheckPlayerPosition();
        }
    }

    void GenerateVector(ref GameObject obj)
    {
        float forceToApply = ((sw.ElapsedMilliseconds / 1000) * 50f);
        UnityEngine.Debug.unityLogger.Log($"Force to apply: {forceToApply}");
        player p = GameState.getGameState().playerReference;
        if(p.direction == Directions.EAST)
        {
            obj.gameObject.transform.position = new Vector3(p.transform.position.x + startingPosition, p.transform.position.y, 0);
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceToApply, forceToApply));
        }
        else if (p.direction == Directions.WEST)
        {
            obj.gameObject.transform.position = new Vector3(p.transform.position.x + -startingPosition, p.transform.position.y, 0);
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-forceToApply, forceToApply));
        } 
        else if(p.direction == Directions.SOUTH)
        {
            obj.gameObject.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + -startingPosition, 0);
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -forceToApply));
        }
        else
        {
            // north
            obj.gameObject.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + startingPosition, 0);
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, forceToApply));
        }
        obj.gameObject.GetComponent<Rigidbody2D>().angularVelocity = -1000f;
    }

    void CheckPlayerPosition()
    {
        if (GameState.getGameState().playerReference.IsPlayerMoving())
        {
            float playerY = GameState.getGameState().playerReference.transform.position.y;
            float playerX = GameState.getGameState().playerReference.transform.position.x;
            if ((-.55 >= playerY && playerY >= -.80) && (-.50 >= playerX && -.75 >= playerX) && dialogueStarted == false && gameStarted == false)
            {
                this.RequestStartGame();
            }
        }
    }

    void RequestStartGame()
    {
        DialogManager mgr = DialogManager.instance;
        mgr.yesNoBtns.yesButton.onClick.AddListener(this.StartGame);
        mgr.StartDialogue(this.startGameDialogue, this.RestartDialogue, yesNoButtonsEnabled: true);
        this.dialogueStarted = true;
    }

    void StartGame()
    {
        DialogManager mgr = DialogManager.instance;
        mgr.StartDialogue(this.startGameD, yesNoButtonsEnabled: false);
        this.gameStarted = true;
        GameState.getGameState().playerReference.FreezePlayer();
    }

    void RestartDialogue()
    {
        this.dialogueStarted = false;
    }

    void CheckGame()
    {
        if (this.gameStarted)
        {
            if(this.attempts >= this.totalAllowedAttempts)
            {
                if(this.successfullTosses >= this.requiredAttempts)
                {
                    this.Win();
                }
                else
                {
                    this.Lose();
                }
                this.EndGame();
            }
        }
    }

    private void Win()
    {
        DialogManager mgr = DialogManager.instance;
        Dialog winDialogue = new Dialog(new List<string>() { "yay you won!", $"You obtained a Lab kit: {this._event.labKit.Description()}" });
        mgr.StartDialogue(winDialogue);
        this._event.execute();
    }

    private void Lose()
    {
        DialogManager mgr = DialogManager.instance;
        Dialog loseDialogue = new Dialog(new List<string>() { "you lost :( you loser..." });
        mgr.StartDialogue(loseDialogue);
    }

    void EndGame()
    {
        GameState.getGameState().playerReference.UnFreezePlayer();
        this.dialogueStarted = false;
        this.gameStarted = false;
        this.successfullTosses = 0;
        this.attempts = 0;
    }
}
public class PaperTossSuccessEvent : ObjectObtainedEvent
{

    public LabKit labKit;

    public PaperTossSuccessEvent(LabKit kit) : base()
    {
        this.labKit = kit;
    }

    public override void execute()
    {
        player p = GameState.getGameState().playerReference;
        UnityEngine.Debug.unityLogger.Log($"Adding labkit {this.labKit.name()} to inv");
        p.addToInventory(this.labKit);
        GameState.getGameState().levelState.TA_OFFICE.eventToggles["gotLabKit"] = true;
    }
}