using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core.objects.movement_controllers
{
    class Moveable : MonoBehaviour
    {
        protected MovementController _movementController = null;
        public  Vector2 movementSpeed;
        public Vector3 resetPosition;
        private float currentMaxX = 1;
        private float currentMaxY = 1;

        public void InitMovementController()
        {
            // if reset position is not set, ensure it is set to something
            if (this.resetPosition == Vector3.zero)
            {
                this.resetPosition = this.transform.position;
            }
            Debug.unityLogger.Log("Init movement controller for moveable");
            if (this.gameObject.GetComponent<MovementController>() == null)
            {
                this.gameObject.AddComponent<MovementController>();
                this._movementController = this.gameObject.GetComponent<MovementController>();
                this._movementController.objectToControl = this.gameObject;
            }
            else
            {
                Debug.unityLogger.Log("Already has component!");
            }
        }

        public void LoopRandomizedMovement(float maxX, float maxY, int waitSeconds = 1)
        {
            Debug.unityLogger.Log("looping randomized movement?");
            this.currentMaxX = maxX;
            this.currentMaxY = maxY;
            this._movementController.RandomizedMove(maxX, maxY, endMovementCallBack: this.LoopRandomizedMovement, waitSeconds);
        }

        public void LoopRandomizedMovement()
        {
            Debug.unityLogger.Log("looping randomized movement?");
            this.currentMaxX = this._movementController.xdest;
            this.currentMaxY = this._movementController.ydest;
            this.LoopRandomizedMovement(this.currentMaxX, this.currentMaxY, 1);
        }

        public void ResetPosition()
        {
            this.gameObject.transform.position = this.resetPosition;
        }
    }
}
