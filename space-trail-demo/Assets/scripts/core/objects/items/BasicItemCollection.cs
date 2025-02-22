﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core
{
    public class BasicItemCollection : IItem
    {
        [SerializeField]
        public string itemName = "Unnamed item";
        [SerializeField]
        public string ownedBy = "None";
        [SerializeField]
        protected Sprite sprite;
        public List<BasicItem> items = new List<BasicItem>();

        public string name()
        {
            return this.itemName;
        }

        public string id()
        {
            return this.itemName;
        }

        public string spriteName()
        {
            return "default-item-sprite";
        }

        public string description()
        {
            return "some basic item";
        }
    }
}
