using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.gameplay;

namespace Assets.scripts.levels.outside_college_area {
    public class OutsideLecturehall : Level
    {
        public OutsideLecturehall() : base("OutsideLectureHall", false)
        {
            Debug.unityLogger.Log("OutsideLectureHall constructor");
            this.transitionHandlers = new Dictionary<string, Vector3>
            {
                {LevelFactory.HALLWAY, new Vector3(-0.0228f, 2.049f, 0)},
                {LevelFactory.TRANSITION_TO_CITY,  new Vector3(-0.043f, -4.551f, 0)}
            };
        }

        public override void startLevel()
        {
            base.startLevel();
            SpacetrailGame.instance.gameObject.AddComponent<Cloud>();
            this.BootstrapButterflies();
        }

        private void BootstrapButterflies()
        {
            Butterfly.AddButterflyMovement();
        }
    }
}
