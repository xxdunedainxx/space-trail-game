using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using UnityEngine;

namespace Assets.scripts.levels.lecturehall
{
    class BookWithNote : ItemCollection
    {
        [SerializeField]
        public Book book = new Book(Omeed.THE_BOOK_OMEEDS_NOTE_IS_IN);
        [SerializeField]
        public Note note = new Note("Some secret 'no no' note that no one should see");

        public BookWithNote()
        {
            this.itemName = Omeed.THE_BOOK_OMEEDS_NOTE_IS_IN;
            this.ownedBy = "none";
        }
    }
}
