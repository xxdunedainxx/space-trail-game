using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Assets.scripts.core.objects
{
    [Serializable]
    public class BasicNote : BasicItem
    {
        public string Content = "uh oh a secret note!";

        public BasicNote(string content)
        {
            this.Content = content;
            this.itemName = content;
        }
    }
}
