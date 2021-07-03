using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core
{
    [Serializable]
    public class BasicItem : IItem
    {
        public string itemName = "emptyItem";
        public string ownedBy = "None";

        public string name()
        {
            return this.itemName;
        }

        public string id()
        {
            return this.itemName;
        }

    }
}