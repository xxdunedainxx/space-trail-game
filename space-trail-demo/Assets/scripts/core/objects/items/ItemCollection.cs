using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core
{
    public class ItemCollection : MonoBehaviour, IItem
    {
        [SerializeField]
        public string itemName = "Unnamed item";
        [SerializeField]
        public string ownedBy = "None";
        [SerializeField]
        protected Sprite sprite;
        public List<SingleItem> items = new List<SingleItem>();

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
