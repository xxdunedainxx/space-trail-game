using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Assets.scripts.core
{
    [Serializable]
    public class Book : SingleItem
    {
        public string text = "Old man and the sea :)";

        public Book(string text)
        {
            this.text = text;
            this.itemName = text;
        }

    }
}
