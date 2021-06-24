using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core.objects
{
    public class Note: SingleItem
    {
        [SerializeField]
        public string Content = "uh oh a secret note!";

        public Note(string content)
        {
            this.Content = content;
        }
    }
}
