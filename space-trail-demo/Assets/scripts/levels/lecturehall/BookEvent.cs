using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core;
using Assets.scripts.core.objects;

namespace Assets.scripts.levels.lecturehall
{
    // disables the sparkle sprite and gives player the book + omeeds note
    public class BookEvent : ObjectObtainedEvent
    {
        private Bookshelf bookShelfRef;
        private ObjectAnimationHandler sparkle;
        private BasicNote noteItem;
        private BasicBook bookItem;
        private NoteEvent nEvent;

        public BookEvent(BasicBook book, BasicNote note, Bookshelf bookShelf, ObjectAnimationHandler sparkle, ref NoteEvent nEvent) : base()
        {
            this.bookShelfRef = bookShelf;
            this.bookShelfRef.attachedEvent = this;
            this.sparkle = sparkle;
            this.bookItem = book;
            this.noteItem = note;
            this.nEvent = nEvent;
            this.nEvent.setEventInactive();
        }

        public override void execute()
        {
            player p = GameState.getGameState().playerReference;

            p.addToInventory(this.bookItem);
            p.addToInventory(this.noteItem);

            // update bookshelf sprite
            this.bookShelfRef.makeBookshelfEmpty();

            // de-animate
            this.sparkle.disableAnimation();

            this.setEventInactive();

            this.noteDialogue(this.noteItem);
            this.nEvent.setEventActive();
        }

        void noteDialogue(BasicNote n)
        {
            Dialog noteDialogue = new Dialog(new List<string> { $"It appears you obtained a new-ish looking note with the content '{n.Content}'" });
            DialogManager manager = DialogManager.instance;
            manager.StartDialogue(noteDialogue);
        }
    }
}
