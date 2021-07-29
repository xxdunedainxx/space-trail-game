using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core.objects;
using Assets.scripts.core;

public class CollisionTriggerEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ExecuteCollisionEvent()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.unityLogger.Log("enter?");
        if (collision.gameObject.layer == Layers.PLAYER_LAYER_VALUE)
        {
            Debug.unityLogger.Log("Collision with collision trigger!");
            this.ExecuteCollisionEvent();
        }
    }
}
