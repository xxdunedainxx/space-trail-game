using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.ui
{
    class UIObjectFactory : MonoBehaviour
    {
        public Dictionary<string, ObjectAnimationHandler> objectAnimationHandlers = new Dictionary<string, ObjectAnimationHandler>();
        public static UIObjectFactory instance { get; private set; }

        public ObjectAnimationHandler getObjectAnimationHandle(string name)
        {
            return this.objectAnimationHandlers[name];
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }
    }
}
