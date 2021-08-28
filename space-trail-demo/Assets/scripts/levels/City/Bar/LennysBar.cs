using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.ui.asset_loaders;

namespace Assets.scripts.levels
{
    public class LennysBar : Level
    {
        public LennysBar() : base("LennysBar", false)
        {
            Debug.unityLogger.Log("LennysBar constructor");
        }

        public override void startLevel()
        {
            base.startLevel();
            Debug.unityLogger.Log("start lennys bar ...");
        }
    }
}
