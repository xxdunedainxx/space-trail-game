using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.objects;
using Assets.scripts.core.dialogue;

namespace Assets.scripts.levels.lecturehall
{
    public class LevelLectureHall : Level
    {
        private const string INVISIBLE_WALL = "InvisibleWall";
        private const string BOOK_TITLE = "Intro to metereology";
        private const string BOOK_SHELF = "BOOK_SHELF";
        private const string SPARKLE = "";
        public const string OMEEDS_NOTE = "Omeed's Note";
        private const string OMEED = "omeed";
        private const string EMPTY_BOOK_SPRITE = "big-book-shelf-focused-no-book";
        private const string CHALKBOARD = "chalkboard";
        private List<string> Npcs = new List<string> {"omeed", "professor","npc1", "npc2", "npc3", "npc4"};

        public bool levelComplete = false;

        public InvisibleBlock invisibleWall;
        public Bookshelf bookShelf;
        private ObjectAnimationHandler sparkle;
        private BookEvent bEvent;
        private NoteEvent nEvent;
        private BasicBook metereologyBook = new BasicBook(BOOK_TITLE);
        private BasicNote omeedsNote = new BasicNote(OMEEDS_NOTE);
        private ChalkboardDialogue chalkBoard;
        private Omeed omed;
        private List<string> cutSceneDialogue = new List<string>
        {
            "Professor Alex McFadden: Why are asteroids important? Asteroids, like sharks in the ocean, get a bad wrap.",
            "Professor Alex McFadden: Asteroids are destructive building blocks, but building blocks non-theless. Our legacy is as humans, can be traced back to these ‘lifeless’ vagabonds",
            "Professor Alex McFadden: By studying and learning about asteroids, we also inherently learn more about ourselves as humans, and our relationship to the greater universe.",
            "Omeed: uhhhhh professor, if your so smart, can you explain the difference between an asteroid and a meteor?",
            "Professor Alex McFadden: ahhh good question Omeed. A meteor is a small piece of an asteroid that typically burns up prior to impacting a planet.",
            "Professor Alex McFadden: An analogy would be, an asteroid is like your body, and the meteor is like that tiny thing between your legs.",
            "Class: OOOOOOOOOOOO",
            "Omeed: . . . ",
            "Professor Alex McFadden: Well, thats it for todays class everyone. Remember, next weeks exam will be on chapters 5 & 6 PLUS today’s lecture on 7.1.",
            "Professor Alex McFadden: Have a good weekend, and don’t forget any of your things on your way out."
        };
        private CutScene lectureHallCutScene;

        public LevelLectureHall() : base("LectureHall")
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
            this.BuildBookShelf();
            this.sparkle = new ObjectAnimationHandler("sparle-anim0");
            this.omed = GameObject.Find(OMEED).GetComponent<Omeed>();

            this.nEvent = new NoteEvent(this.omeedsNote, ref this.invisibleWall);
            if (GameState.getGameState().levelState.LECTURE_HALL.completed == true)
            {
                this.bookShelf.attachedEvent = new DialogueEvent(new List<string>() { "Looks like a normal book shelf..." }, "bookshelf-dialogue");
                this.nEvent.setEventInactive();
            }
            else
            {
                this.bEvent = new BookEvent(this.metereologyBook, this.omeedsNote, this.bookShelf, this.sparkle, ref this.nEvent);
            }
            GameObject.Find(CHALKBOARD).AddComponent<ChalkboardDialogue>();
            this.chalkBoard = GameObject.Find(CHALKBOARD).GetComponent<ChalkboardDialogue>();
            this.chalkBoard.lectureHallRef = this;


            this.omed.nEvent = this.nEvent;
            Debug.unityLogger.Log("Done initializing lecture hall");

            if (GameState.getGameState().levelState.LECTURE_HALL.completed == true)
            {
                GameState.getGameState().playerReference.transform.position = new Vector3(12.293f, -0.794f, 0);
                this.gameState.playerReference.animator.Play("PlayerFacingLeft");
                this.invisibleWall.Blocking = false;
                this.sparkle.disableAnimation();
                this.bookShelf.makeBookshelfEmpty();
                Level.DisableLevelObjects(this.Npcs);
            }
            else
            {
                this.lectureHallCutScene = new CutScene(
                    new Dialog(this.cutSceneDialogue, 0.1f)
                );
                GameState.getGameState().playerReference.transform.position = new Vector3((float)9.85, (float)-.069, 0);
                this.lectureHallCutScene.RunCutScene();
            }
        }

        private void BuildBookShelf()
        {
            GameObject bookShelfGameObject = GameObject.Find(BOOK_SHELF);
            bookShelfGameObject.AddComponent<Bookshelf>();
            this.bookShelf = GameObject.Find(BOOK_SHELF).GetComponent<Bookshelf>();
            this.bookShelf.body = bookShelfGameObject.transform;
            Sprite s = Resources.Load<Sprite>("Images/chapters/chapter1/Levels/LectureHall/book-shelf-new");
            Debug.unityLogger.Log($"Sprite {s}");
            this.bookShelf.defaultBookShelf = s;
            this.bookShelf.emptyBookShelf = Resources.Load<Sprite>("Images/chapters/chapter1/Levels/LectureHall/book-shelf-new-book-removed");
            Debug.unityLogger.Log($"Bookshelf images: {this.bookShelf.defaultBookShelf}/ {this.bookShelf.emptyBookShelf}");
        }
    }
}