using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.ui
{
    class AnimationCollisionTrigger : CollisionTriggerEvent
    {
        public string gameObjectToAnimate;
        public string animatorToPlay;

        public override void ExecuteCollisionEvent()
        {
            Animator anim = GameObject.Find(this.gameObjectToAnimate).GetComponent<Animator>();
            anim.enabled = true;
            anim.Play(this.animatorToPlay);
        }
    }
}
