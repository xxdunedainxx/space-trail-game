using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;

public class ObtainableObject : MonoBehaviour, IClickable, IEventEmitter
{

    public BasicItem item = null;
    [SerializeField]
    public ObjectAnimationHandler associatedAnimation = null;

    private List<IEventConsumer> consumers = new List<IEventConsumer>();

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void click()
    {
        Debug.unityLogger.Log("ObtainableObject clicked");
    }

    public void emitEvent(IEvent even)
    {
       foreach(IEventConsumer consumer in this.consumers)
        {
            consumer.consumeEvent(even);
        }
    }

    public void addConsumer(IEventConsumer consumer)
    {
        this.consumers.Add(consumer);
    }
}
