using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.ui.asset_loaders;
using Assets.scripts.core.gameplay;
using Assets.scripts.levels.outside_college_area;
using Assets.scripts.core.objects.movement_controllers;

namespace Assets.scripts.levels
{
    public class City : Level
    {
        private Vector3 carResetPositionHorizontal = new Vector3(4.34f, 0.45f, 0f);
        private readonly string oldLadyName = "old-lady-v1";
        private OldLady oldLadyObject = null;

        public City() : base("city-area", false)
        {
            Debug.unityLogger.Log("City constructor");
            this.transitionHandlers = new Dictionary<string, Vector3>
            {
                {LevelFactory.OUTSIDE_LECTUREHALL, new Vector3(-0.7f, 4.47f, 0)},
                {LevelFactory.LENNYS_BAR,  new Vector3(-2.692f, -4.243f, 0)}
            };
        }

        private void BootstrapOldLady()
        {
            GameObject.Find(this.oldLadyName).AddComponent<OldLady>();
            this.oldLadyObject = GameObject.Find(this.oldLadyName).GetComponent<OldLady>();
        }

        private bool CrossStreetGameActive()
        {
            return GameState.getGameState().levelState.CITY.eventToggles["crossStreet"];
        }

        public override void startLevel()
        {
            base.startLevel();
            Debug.unityLogger.Log("start city ...");
            Butterfly.AddButterflyMovement();
            this.InitCars();
            this.BootstrapOldLady();

            if (CrossStreetGameActive())
            {
                CrossStreetGameEvent game =  new CrossStreetGameEvent();
                game.SetOldLadyReference(ref this.oldLadyObject);
                this.oldLadyObject.AddEventListener(game);
                this.oldLadyObject.ShakeAnimation();
            } else
            {
                this.oldLadyObject.gameObject.SetActive(false);
            }
        }

        public void InitCars()
        {
            foreach(GameObject car in GameObject.FindGameObjectsWithTag("horizontal-car"))
            {
                car.AddComponent<Car>();
                Car cObject = car.GetComponent<Car>();
                cObject.resetPosition = this.carResetPositionHorizontal;
                cObject.West();
                cObject.Go();
            }
        }
    }
}
