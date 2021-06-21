using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public string ownedBy = "None";
        [SerializeField]
        protected Sprite sprite;
    }
}
