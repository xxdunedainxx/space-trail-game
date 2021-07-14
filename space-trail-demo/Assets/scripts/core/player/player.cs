using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts;
using Assets.scripts.core;
using System.Threading;
using System;

public class player : MonoBehaviour
{
    public string direction;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform feet;
    public Transform head;
    public Transform body;
    public LayerMask groundLayers;
    public LayerMask npcDialog;
    [SerializeField]
    public Sprite downImage;
    [SerializeField]
    public Sprite rightImage;
    [SerializeField]
    public Sprite rightImageMoving;
    [SerializeField]
    public Sprite upImage;
    [SerializeField]
    public Sprite leftImage;
    [SerializeField]
    public Sprite leftImageMoving;

    private Camera playerCam;

    public float mx;
    public float my;

    public bool sideScrolling = false;

    private GamePreferences preferences = null;
    private PlayerState state = new PlayerState();

    public bool ready = false;

    private bool playerFreeze = false;

    public PlayerState getPlayerState()
    {
        return this.state;
    }

    public void setPlayerState(PlayerState state)
    {
        Debug.unityLogger.Log($"Setting player state to {state}");
        this.state = state;
    }

    private bool playerReady()
    {
        if(this.state == null || this.preferences == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    IEnumerator waitPlayerReady()
    {
        yield return new WaitUntil(playerReady);
    }

    private void Awake()
    {
        Debug.unityLogger.Log("setting player reference in gamestate");
        GameState.getGameState().playerReference = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       this.playerCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void initPlayer()
    {
        Debug.unityLogger.Log("Start new Player Object");
        this.preferences = GamePreferences.getPreferences();
        if (this.state.inventory == null)
        {
            Debug.unityLogger.Log("inventory is null, initializing");
            this.state.inventory = new List<BasicItem>();
        }

        GameState.getGameState().setReady();
        // this.__logger.Log("Start new player object");
    }

    public bool AllowPlayerMovement()
    {
        return this.playerFreeze == false;
    }

    public void FreezePlayer()
    {
        this.playerFreeze = true;
    }

    public void UnFreezePlayer()
    {
        this.playerFreeze = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerReady() == false)
        {
            Debug.unityLogger.Log("player not ready yet...");
            return;
        }

        if(AllowPlayerMovement() == false)
        {
            Debug.unityLogger.Log("player not allowed to move");
            return;
        }


        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        if(sideScrolling && Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            Jump();
        }

        if (Input.GetMouseButtonDown(0)) {
            Debug.unityLogger.Log("They clicked left");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                Debug.unityLogger.Log($"Hit {hit.transform.name}");
                if (hit.transform.name == "")
                {

                }
            }
            /*if(interact())
            {
                Debug.unityLogger.Log("They are trying to interact with something.");
            }*/
        }

        if(Input.GetKeyDown(this.preferences.inputs.moveDown))
        {
            //this.animator.SetInteger("movingDirection", Movements.DownMoving);
            this.animator.Play("PlayerFacingCamera");
            this.direction = Directions.SOUTH;
            this.body.transform.rotation = Quaternion.Euler(
                90f, 0f, 0f
            );
        }
        else if (Input.GetKeyUp(this.preferences.inputs.moveRight))
        {
            //this.animator.SetInteger("movingDirection", Movements.Right);
            this.animator.Play("PlayerFacingRight");
            this.direction = Directions.EAST;
        }
        else if (Input.GetKeyDown(this.preferences.inputs.moveRight))
        {
            //this.animator.SetInteger("movingDirection", Movements.RightMoving);
            this.body.transform.rotation = Quaternion.Euler(
                0f, 0f, 0f
            );
            this.animator.Play("PlayerMovingRight");
            this.direction = Directions.EAST;
        }
        else if (Input.GetKeyDown(this.preferences.inputs.moveLeft))
        {
            //this.animator.SetInteger("movingDirection", Movements.LeftMoving);
            this.body.transform.Rotate(180f, 0f, 0f);
            this.animator.Play("PlayerMovingLeft");
            this.direction = Directions.WEST;
            this.body.transform.rotation = Quaternion.Euler(
                180f, 0f, 0f
            );
        }
        else if (Input.GetKeyUp(this.preferences.inputs.moveLeft))
        {
            //this.animator.SetInteger("movingDirection", Movements.LeftMoving);
            this.animator.Play("PlayerFacingLeft");
            this.direction = Directions.WEST;
        }
        else if (Input.GetKeyDown(this.preferences.inputs.moveUp))
        {
            //this.animator.SetInteger("movingDirection", Movements.UpMoving);
            this.animator.Play("PlayerFacingAwayCamera");
            this.direction = Directions.NORTH;
            this.body.transform.rotation = Quaternion.Euler(
                270f, 0f, 0f
            );
        }
    }

    private void FixedUpdate()
    {
        float yValue = sideScrolling ? rb.velocity.y : my * this.state.movementSpeed;
        Vector2 movement = new Vector2(mx * this.state.movementSpeed, yValue);  //rb.velocity.y  my * movementSpeed

        rb.velocity = movement;
    }

    private void Jump() {
        Vector2 movement = new Vector2(rb.velocity.x, this.state.jumpForce);

        rb.velocity = movement;
    }

    public bool IsGrounded() {
        Collider2D groundChecking = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        if (groundChecking != null) {
            return true;
        }
        return false;
    }

    public bool interact()
    {
        Collider2D interactCheck = Physics2D.OverlapCircle(this.head.position, 0.5f, this.npcDialog);

        if (interactCheck)
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.unityLogger.Log("Colided with objecty?");
        Debug.unityLogger.Log($"{other.gameObject.layer}");
        /*if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
        }*/
    }

    public void TriggerDialogue()
    {
        DialogManager mgr = DialogManager.instance;
    }

    public void addToInventory(BasicItem item)
    {
        Debug.unityLogger.Log($"Adding single item to inv {item.name()}");
        this.state.inventory.Add(item);
        this.printUserInventory();
    }

    public void addToInventory(BasicItemCollection items)
    {
        Debug.unityLogger.Log("Item collection, will iterate and add all itmes to user inv");
        foreach (BasicItem item in items.items)
        {
            this.state.inventory.Add(item);
        }
        this.printUserInventory();
    }

    public void removeFromInventory(string itemID)
    {
       foreach(BasicItem item in this.state.inventory)
        {
            if(item.name() == itemID)
            {
                Debug.unityLogger.Log($"removing {item.name()} from inv");
                this.state.inventory.Remove(item);
                return;
            }
        }
    }

    public List<BasicItem> getInventory()
    {
        return this.state.inventory;
    }

    private void printUserInventory()
    {
        Debug.unityLogger.Log($"{this.name}'s current inventory:");
        for(int i = 0; i < this.state.inventory.Count; i++)
        {
            Debug.unityLogger.Log($"{ this.state.inventory[i].name()}");
        }
    }
}

[Serializable]
public class PlayerState
{
    public List<BasicItem> inventory = new List<BasicItem>();
    public string playerName = "player1";
    public float jumpForce = 20f;
    public float movementSpeed = 1f;

    public PlayerState()
    {
    }

    public void printPlayerState()
    {
        string pLine = $@"
            ==== PLAYER STATE ====
            Inventory: {printInventory()},
            Name: {playerName},
            jumpForce: {jumpForce},
            movement: {movementSpeed}
        ";
        Debug.unityLogger.Log(pLine);
    }

    public string printInventory()
    {
        if(this.inventory == null)
        {
            return "empty inventory..";
        }
        string sbuilder = "";
        foreach(BasicItem itm in this.inventory)
        {
            sbuilder += $"{itm.name()}\n";
        }
        return sbuilder;
    }
}