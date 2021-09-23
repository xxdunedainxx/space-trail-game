using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core.objects.movement_controllers
{
    class GlassShadowController : Moveable
    {
        public float maxX = .5f;
        public float xDirectionSpeed = 0.15f;

        public void Start()
        {
            this.movementSpeed = new Vector2(this.xDirectionSpeed, 0);
            this.InitMovementController();
            
        }

        public void StartRandomizedMovement()
        {
            if(this._movementController == null)
            {
                this.InitMovementController();
            }
            this._movementController.LayerBound = Layers.BOUNDRY_LAYER_VALUE;
            this.SetMovementControllerSpeed();
            this.LoopRandomizedMovement(this.maxX, 0);
        }

        public void FlipRandomizeMove()
        {
            this.currentMaxX = -this.currentMaxX;
            this.currentMaxY = -this.currentMaxY;

            this.SetMovementControllerSpeed();

            this._movementController.RandomizedMove(this.currentMaxX, this.currentMaxY, endMovementCallBack: this.FlipRandomizeMove, this.waitSeconds);
        }

        public override void LoopRandomizedMovement(float maxX, float maxY, int waitSeconds = 1)
        {
            Debug.unityLogger.Log("looping randomized movement?");
            this.currentMaxX = maxX;
            this.currentMaxY = maxY;
            this.waitSeconds = waitSeconds;
            this._movementController.SetMovementSpeed(new Vector2(this.xDirectionSpeed, 0));
            this._movementController.RandomizedMove(maxX, maxY, endMovementCallBack: this.FlipRandomizeMove, waitSeconds);
        }

    }
}
