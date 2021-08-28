using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.ui.asset_loaders;

namespace Assets.scripts.levels
{
    public class City : Level
    { 
        public City() : base("city-area", false)
        {
            Debug.unityLogger.Log("City constructor");
        }

        public override void startLevel()
        {
            base.startLevel();
            Debug.unityLogger.Log("start city ...");
        }
    }
}
