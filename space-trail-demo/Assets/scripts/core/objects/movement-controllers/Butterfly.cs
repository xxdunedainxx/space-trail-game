using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.levels.outside_college_area
{
    class Butterfly : MonoBehaviour
    {
        private MovementController _moveButterflyController;
        public float maxMovementRange;
        public Vector2 movementSpeed = new Vector2(.5f, .5f);
        // Start is called before the first frame update

        public static void AddButterflyMovement()
        {
            foreach(GameObject butterflyObject in GameObject.FindGameObjectsWithTag("butterfly"))
            {
                butterflyObject.AddComponent<Butterfly>();
            }
        }

        void Start()
        {
            this.gameObject.AddComponent<MovementController>();
            this._moveButterflyController = this.gameObject.GetComponent<MovementController>();
            this._moveButterflyController.objectToControl = this.gameObject;
            this.MoveButterFly();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MoveButterFly()
        {
            this.LoopRandomizedMovement();
        }

        public void LoopRandomizedMovement()
        {
            Debug.unityLogger.Log("looping randomized movement?");
            this._moveButterflyController.RandomizedMove(1, 1, endMovementCallBack: this.LoopRandomizedMovement, 1);
        }
    }
}
