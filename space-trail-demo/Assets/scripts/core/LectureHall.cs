using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;
using Assets.scripts.core.objects;
public class LectureHall : MonoBehaviour, ILevel, IEventConsumer
{
    private const string INVISIBLE_WALL = "InvisibleWall";
    private const string BOOK_SHELF = "big-book-shelf-focused 1";
    public bool levelComplete = false;
    public GameObject invisibleWall;
    public Bookshelf bookShelf;

    public void consumeEvent(IEvent even)
    {
        Debug.unityLogger.Log($"Event received for lecture hall.. {even.GetType()}");
        ObjectObtainedEvent obj = (ObjectObtainedEvent)even;
        Debug.unityLogger.Log($"Obtainable object {obj.name}, {obj.obtained}");
        if(obj.obtained && obj.name == BOOK_SHELF)
        {
            this.completeLevel();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.unityLogger.Log("Initializing LectureHall level..");
        // add the 'invisible wall' dependency
        this.invisibleWall = GameObject.Find(INVISIBLE_WALL);
        this.bookShelf = GameObject.Find(BOOK_SHELF).GetComponent<Bookshelf>();
        this.bookShelf.addConsumer(this);
        Debug.unityLogger.Log("Done initializing lecture hall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void completeLevel()
    {
        this.invisibleWall.gameObject.SetActive(false);
    }
}
