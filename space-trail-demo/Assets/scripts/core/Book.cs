using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.scripts.core
{
    public class Book : SingleItem
    {
        [SerializeField]
        public string text = "Old man and the sea :)";

        public Book(string text)
        {
            this.text = text;
        }
    }
}
