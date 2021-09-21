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
        public float maxX = 1;

        public void Start()
        {
            this.InitMovementController();
            
        }

        public void StartRandomizedMovement()
        {
            if(this._movementController == null)
            {
                this.InitMovementController();
            }
            this._movementController.LayerBound = Layers.BOUNDRY_LAYER_VALUE;
            this._movementController.SetMovementSpeed(new Vector2(0.15f,0));
            this.LoopRandomizedMovement(this.maxX, 0);
        }

    }
}
