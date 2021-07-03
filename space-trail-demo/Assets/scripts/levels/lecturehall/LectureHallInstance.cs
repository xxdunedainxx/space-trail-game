using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.objects;

namespace Assets.scripts.levels.lecturehall
{
    public class LectureHallInstance : Level
    {
        private const string INVISIBLE_WALL = "InvisibleWall";
        private const string BOOK_SHELF = "big-book-shelf-focused 1";
        private const string BOOK_TITLE = "Intro to metereology";
        private const string SPARKLE = "";
        private const string OMEEDS_NOTE = "Omeed's Note";
        private const string OMEED = "omeed";
        private const string EMPTY_BOOK_SPRITE = "big-book-shelf-focused-no-book";

        public bool levelComplete = false;

        public InvisibleBlock invisibleWall;
        public Bookshelf bookShelf;
        private ObjectAnimationHandler sparkle;
        private BookEvent bEvent;
        private NoteEvent nEvent;
        private BasicBook metereologyBook = new BasicBook(BOOK_TITLE);
        private BasicNote omeedsNote = new BasicNote(OMEEDS_NOTE);
        private Omeed omed;

        public LectureHallInstance() : base("LectureHall")
        {
            Debug.unityLogger.Log("LectureHall constructor");
        }

        // Start is called before the first frame update
        void Start()
        {


            // new Vector3((float)-5.84, (float)-1.18, 0);
        }

        public override void startLevel()
        {
            Debug.unityLogger.Log("Initializing LectureHall level..");
            // add the 'invisible wall' dependency
            this.invisibleWall = GameObject.Find(INVISIBLE_WALL).GetComponent<InvisibleBlock>();
            this.bookShelf = GameObject.Find(BOOK_SHELF).GetComponent<Bookshelf>();
            this.sparkle = new ObjectAnimationHandler("sparle-anim0");
            this.omed = GameObject.Find(OMEED).GetComponent<Omeed>();

            this.nEvent = new NoteEvent(this.omeedsNote, ref this.invisibleWall);
            this.bEvent = new BookEvent(this.metereologyBook, this.omeedsNote, this.bookShelf, this.sparkle, ref this.nEvent);

            this.omed.nEvent = this.nEvent;
            Debug.unityLogger.Log("Done initializing lecture hall");

            if (GameState.getGameState().levelState.LECTURE_HALL.completed == true)
            {
                GameState.getGameState().playerReference.transform.position = new Vector3((float)-1.29, (float)-3.63, 0);
                this.gameState.playerReference.animator.Play("PlayerFacingLeft");
                this.invisibleWall.Blocking = false;
                this.sparkle.disableAnimation();
                this.bookShelf.makeBookshelfEmpty();
            }
            else
            {
                GameState.getGameState().playerReference.transform.position = new Vector3((float)-5.84, (float)-1.18, 0);
            }
        }
    }
}