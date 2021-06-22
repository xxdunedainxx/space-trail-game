using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.objects;

namespace Assets.scripts.levels.lecturehall
{
    public class LectureHall : MonoBehaviour, ILevel, IEventConsumer
    {
        private const string INVISIBLE_WALL = "InvisibleWall";
        private const string BOOK_SHELF = "big-book-shelf-focused 1";
        private const string OMEEDS_NOTE = "Omeed's Note";
        public bool levelComplete = false;
        public GameObject invisibleWall;
        public Bookshelf bookShelf;

        // Start is called before the first frame update
        void Start()
        {
            Debug.unityLogger.Log("Initializing LectureHall level..");
            // add the 'invisible wall' dependency
            this.invisibleWall = GameObject.Find(INVISIBLE_WALL);
            this.bookShelf = GameObject.Find(BOOK_SHELF).GetComponent<Bookshelf>();
            this.bookShelf.addConsumer(this);
            //this.initBookShelf();
            Debug.unityLogger.Log("Done initializing lecture hall");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void consumeEvent(IEvent even)
        {
            Debug.unityLogger.Log($"Event received for lecture hall.. {even.GetType()}");
            ObjectObtainedEvent obj = (ObjectObtainedEvent)even;
            Debug.unityLogger.Log($"Obtainable object {obj.item.name()}, {obj.obtained}");

            if(obj.obtained && obj.item.name() == OMEEDS_NOTE)
            {
                this.noteDialogue((Note)obj.item);
            }
        }

        public void completeLevel()
        {
            this.invisibleWall.gameObject.SetActive(false);
        }

        void noteDialogue(Note n)
        {
            Dialog noteDialogue = new Dialog(new List<string> { $"It appears you obtained a new-ish looking note with the content '{n.Content}'" });
            DialogManager manager = DialogManager.instance;
            manager.StartDialogue(noteDialogue);
        }

        /*private void initBookShelf()
        {
            Debug.unityLogger.Log("Adding bookwithnote to bookshelf");
            Bookshelf shelfScript = this.bookShelf.gameObject.GetComponent<Bookshelf>();
            shelfScript.books.Add(new BookWithNote());
            Debug.unityLogger.Log("DONE Adding bookwithnote to bookshelf");
        }*/
    }
}