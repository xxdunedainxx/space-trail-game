using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransitionHandler : MonoBehaviour
{
    public static LevelTransitionHandler instance { get; private set; }
    private Canvas transitionCanvas;
    private Image transitionImage;
    public Animator transitionAnimator;

    [SerializeField]
    public Dictionary<string, string> GameObjectLookupTable = new Dictionary<string, string>() {
        {"TransitionBackgroundParentObject","BackgroundFader" },
        {"TransitionBackgroundCanvas", "BackgroundFaderCanvas" },
        { "TransitionBackgroundImage", "BackgroundFaderImage" }
    };
    // Start is called before the first frame update
    void Start()
    {
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
            this.InitializeGameObjects();
        }
    }

    private void InitializeGameObjects()
    {
        this.transitionCanvas = GameObject.Find(GameObjectLookupTable["TransitionBackgroundCanvas"]).GetComponent<Canvas>();
        this.transitionImage = GameObject.Find(GameObjectLookupTable["TransitionBackgroundImage"]).GetComponent<Image>();
        this.transitionAnimator = GameObject.Find(GameObjectLookupTable["TransitionBackgroundImage"]).GetComponent<Animator>();
    }

    public void Transition()
    {
        this.transitionCanvas.enabled = true;
        this.transitionImage.enabled = true;
        this.transitionAnimator.enabled = true;
        this.transitionAnimator.Play("BlackFadeAnimation");
        // todo add a transition timer?

        Debug.unityLogger.Log("Transitioner running?");
        //this.EndTransition();
    }

    public void EndTransition()
    {
        this.transitionCanvas.enabled = false;
        this.transitionImage.enabled = false;
        this.transitionAnimator.enabled = true;
    }
}
