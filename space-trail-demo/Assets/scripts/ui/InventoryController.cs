using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, string> GameObjectLookupTable = new Dictionary<string, string>() {
        {"SaveButton","SaveButton" },
        {"SaveButtonText","SaveButtonText" },
        { "OptionsButtonText","OptionsButtonText"},
        {"OptionsButton", "OptionsButton" },
        {"InventoryButtonText","InventoryButtonText" },
        {"InventoryButton","InventoryButton" },
        {"InventoryUI","InventoryUI" },
        {"InventoryEscapeButton","InventoryEscapeButton" }
    };

    private Canvas UI;
    private CanvasRenderer inventoryUI;
    private Button save;
    private Text saveText;
    private Button options;
    private Text optionsText;
    private Button inventory;
    private Text inventoryText;
    private Button close;
    private Button inventoryEscape;
    private List<UIInventoryItem> inventoryItems = new List<UIInventoryItem>();

    private Dialog saveDialogue = new Dialog(new List<string> { "Saved!" });
    private bool inventoryBuilt = false;

    // Start is called before the first frame update
    void Start()
    {
        this.InitializeGameObjects();
        this.AddOnClickListeners();
        GameState.getGameState().playerReference.inventoryController = this;
    }

    private void InitializeGameObjects()
    {
        this.save = GameObject.Find(GameObjectLookupTable["SaveButton"]).GetComponent<Button>();
        this.saveText = GameObject.Find(GameObjectLookupTable["SaveButtonText"]).GetComponent<Text>();

        this.options = GameObject.Find(GameObjectLookupTable["OptionsButton"]).GetComponent<Button>();
        this.optionsText = GameObject.Find(GameObjectLookupTable["OptionsButtonText"]).GetComponent<Text>();

        this.inventory = GameObject.Find(GameObjectLookupTable["InventoryButton"]).GetComponent<Button>();
        this.inventoryText = GameObject.Find(GameObjectLookupTable["InventoryButtonText"]).GetComponent<Text>();
        this.inventoryUI = GameObject.Find(GameObjectLookupTable["InventoryUI"]).GetComponent<CanvasRenderer>();
        this.inventoryEscape = GameObject.Find(GameObjectLookupTable["InventoryEscapeButton"]).GetComponent<Button>();

        this.UI = GameObject.Find("Inventory").GetComponent<Canvas>();
        this.close = GameObject.Find("CloseInventory").GetComponent<Button>();
        StartCoroutine(this.WaitBuildInventory());
        StartCoroutine(this.WaitToCloseOnStart());
    }

    private void AddOnClickListeners()
    {
        this.save.onClick.AddListener(this.Save);
        this.options.onClick.AddListener(this.Options);
        this.inventory.onClick.AddListener(this.Inventory);
        this.close.onClick.AddListener(this.Close);
        this.inventoryEscape.onClick.AddListener(this.DisableInventoryComponents);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        Persistence.PersistData();
        DialogManager.instance.StartDialogue(this.saveDialogue);
    }

    public void Inventory()
    {
        this.CloseAllButCanvas();
        this.EnableInventoryComponents();
    }

    private bool StateIsReady()
    {
        return GameState.getGameState().playerReference.playerReady() && Persistence.CheckInitialized();
    }

    private bool InventoryReady()
    {
        return this.inventoryBuilt;
    }

    IEnumerator WaitToCloseOnStart()
    {
        yield return new WaitUntil(InventoryReady);
        this.Close();
    }


    IEnumerator WaitBuildInventory()
    {
        yield return new WaitUntil(StateIsReady);
        Debug.unityLogger.Log("state is ready!");
        this.BuildInventory();
    }

    private void BuildInventory()
    {
        Debug.unityLogger.Log("building inventory...");
        List<BasicItem> playerInv = GameState.getGameState().playerReference.getPlayerState().inventory;
        GameObject[] inventoryObjects = GameObject.FindGameObjectsWithTag("inventoryItem");
        Debug.unityLogger.Log($"Inventory: {inventoryObjects} | Player inv : {playerInv} | count {playerInv.Count}");
        for(int i = 0; i < inventoryObjects.Length; i++)
        {
            Image invImage = inventoryObjects[i].GetComponent<Image>();
            Button invBtn = inventoryObjects[i].GetComponent<Button>();

            inventoryObjects[i].AddComponent<UIInventoryItem>();
            UIInventoryItem uiItem = inventoryObjects[i].GetComponent<UIInventoryItem>();
            uiItem.originalInvImage = invImage;
            uiItem.inventoryObjectButton = invBtn;
            Debug.unityLogger.Log($"i {i} vs player inv count {playerInv.Count}");
            if(i < playerInv.Count)
            {
                Debug.unityLogger.Log($"adding player inventory item {playerInv[i].name()}");
                uiItem.associatedItem = playerInv[i];
                uiItem.inventorySprite = Resources.Load<Sprite>($"Images/shared/items/{uiItem.associatedItem.spriteName()}");
                uiItem.originalInvImage.sprite = uiItem.inventorySprite;
            } 
            uiItem.inventoryObjectButton.onClick.AddListener(uiItem.PrintItemInfo);
            this.inventoryItems.Add(uiItem);
        }
        this.inventoryBuilt = true;
    }

    void EnableInventoryItems()
    {
        foreach(UIInventoryItem item in this.inventoryItems)
        {
            item.Enable();
        }
    }

    void DisableInventoryItems()
    {
        foreach (UIInventoryItem item in this.inventoryItems)
        {
            item.Disable();
        }
    }

    public void Options()
    {
        this.CloseAllButCanvas();
    }

    public void EnableInventoryComponents()
    {
        this.inventoryUI.GetComponent<Image>().enabled = true;
        this.inventoryEscape.enabled = true;
        this.inventoryEscape.GetComponent<Image>().enabled = true;
        this.EnableInventoryItems();
    }

    public void DisableInventoryComponents()
    {
        this.inventoryUI.GetComponent<Image>().enabled = false;
        this.inventoryEscape.enabled = false;
        this.inventoryEscape.GetComponent<Image>().enabled = false;
        this.DisableInventoryItems();
        this.Enable();
    }

    public void EnableOptionsComponents()
    {

    }

    public void DisableOptionsComponents()
    {

    }

    private void CloseAllButCanvas()
    {
        this.save.enabled = false;
        this.saveText.enabled = false;

        this.options.enabled = false;
        this.optionsText.enabled = false;

        this.inventory.enabled = false;
        this.inventoryText.enabled = false;
        this.DisableInventoryComponents();
        this.DisableOptionsComponents();
    }

    public void Close()
    {
        this.CloseAllButCanvas();

        this.UI.enabled = false;
        this.close.enabled = false;
    }

    public void Enable()
    {
        this.save.enabled = true;
        this.saveText.enabled = true;

        this.options.enabled = true;
        this.optionsText.enabled = true;

        this.inventory.enabled = true;
        this.inventoryText.enabled = true;

        this.UI.enabled = true;
        this.close.enabled = true;
    }
}
