using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.core
{
    class Inventory
    {
        private List<IItem> _inventory;
        int capacity;
        public Inventory(List<IItem> items)
        {
            this._inventory = items;
        }
    }
}
