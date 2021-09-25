using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.ui.asset_loaders;

namespace Assets.scripts.levels
{
    public class EightTwelve : Level
    {
        public EightTwelve() : base("inside-8-12", false)
        {
            Debug.unityLogger.Log("EightTwelve constructor");
        }

        public override void startLevel()
        {
            base.startLevel();
            Debug.unityLogger.Log("start 8 twelve ...");
        }
    }
}
