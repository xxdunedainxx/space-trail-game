using Assets.scripts.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.core.objects
{
    class TransitionWall : MonoBehaviour
    {
        [SerializeField]
        string sceneToTransitionTo;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.unityLogger.Log($"transition wall: {collision.collider.gameObject.layer}, vs {Layers.PLAYER_LAYER_VALUE}");
            if(collision.collider.gameObject.layer == Layers.PLAYER_LAYER_VALUE)
            {
                Level.levelTransition(sceneToTransitionTo);
            }
            
            
        }
    }
}
