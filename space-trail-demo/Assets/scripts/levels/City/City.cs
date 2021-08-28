using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.ui.asset_loaders;
using Assets.scripts.core.gameplay;

namespace Assets.scripts.levels
{
    public class City : Level
    { 
        public City() : base("city-area", false)
        {
            Debug.unityLogger.Log("City constructor");
            this.transitionHandlers = new Dictionary<string, Vector3>
            {
                {LevelFactory.OUTSIDE_LECTUREHALL, new Vector3(-0.7f, 4.47f, 0)},
                {LevelFactory.LENNYS_BAR,  new Vector3(-2.692f, -4.243f, 0)}
            };
        }

        public override void startLevel()
        {
            base.startLevel();
            Debug.unityLogger.Log("start city ...");
        }
    }
}
