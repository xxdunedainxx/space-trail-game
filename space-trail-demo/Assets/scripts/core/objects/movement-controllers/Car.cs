using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core.objects.movement_controllers
{
    class Car : Moveable
    {
        /// <summary>
        /// Things to consider: movement around other cars, consider the player, etc 
        /// </summary>
        public float directionalSpeed = 3f;
        public string currentMovementFlag = MovementController.MovementFlags.NEUTRAL;
        

        public void Start()
        {
            this.InitMovementController();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Wait()
        {
            this.currentMovementFlag = MovementController.MovementFlags.STOP;
        }

        public void StopLight()
        {
            this.Wait();
        }

        public string Evaluate()
        {
            return this.currentMovementFlag;
        }

        public void Go()
        {
            Debug.unityLogger.Log("start car!");
            if(this._movementController == null)
            {
                this.InitMovementController();
            }
            StartCoroutine(this._movementController.Move(this.movementSpeed, this.Evaluate));
            Debug.unityLogger.Log("car started");
        }

        public void Startup()
        {
            this.currentMovementFlag = MovementController.MovementFlags.CHANGE_SPEED;
        }

        public void ObjectInWay()
        {
            this.Wait();
        }

        public void West()
        {
            this.movementSpeed = new Vector2(-this.directionalSpeed, 0);
        }

        public void East()
        {
            this.movementSpeed = new Vector2(this.directionalSpeed, 0);
        }

        public void North()
        {
            this.movementSpeed = new Vector2(0, this.directionalSpeed);
        }

        public void South()
        {
            this.movementSpeed = new Vector2(0, -this.directionalSpeed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.gameObject.tag == "boundary")
            {
                this.ResetPosition();
                this.Go();
            }
            else
            {
                this.ObjectInWay();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            this.Startup();
        }
    }
}
