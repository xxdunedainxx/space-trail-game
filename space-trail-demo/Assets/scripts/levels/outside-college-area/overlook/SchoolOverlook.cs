using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.ui.asset_loaders;
using Assets.scripts.levels;

namespace Assets.scripts.levels.outside_college_area.overlook
{
    class SchoolOverlook : Level
    {
        public SchoolOverlook() : base("inside-8-12", false)
        {
            Debug.unityLogger.Log("SchoolOverlook constructor");
        }

        public override void startLevel()
        {
            base.startLevel();
            Debug.unityLogger.Log("start SchoolOverlook ...");
        }
    }
}
