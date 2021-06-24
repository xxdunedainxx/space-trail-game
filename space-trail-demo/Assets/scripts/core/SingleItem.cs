using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core
{
    public class SingleItem : MonoBehaviour, IItem
    {
        [SerializeField]
        public string itemName = "emptyItem";
        [SerializeField]
        public string ownedBy = "None";
        [SerializeField]
        protected Sprite sprite;

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
