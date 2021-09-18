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

        public void InitMovementController()
        {
            this.resetPosition = this.transform.position;
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

        public void LoopRandomizedMovement()
        {
            Debug.unityLogger.Log("looping randomized movement?");
            this._movementController.RandomizedMove(1, 1, endMovementCallBack: this.LoopRandomizedMovement, 1);
        }

        public void ResetPosition()
        {
            this.gameObject.transform.position = this.resetPosition;
        }
    }
}
