﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.core.events;

namespace Assets.scripts.core.objects
{
    public class ObjectObtainedEvent : IEvent
    {
        
        public BasicItem item;
        bool isActive = true;
        List<EventLookupInfo> dependentEvents;

        public ObjectObtainedEvent(BasicItem item)
        {
            this.item = item;
        }

        public ObjectObtainedEvent()
        {
            this.item = new BasicItem();
        }

        public virtual string name()
        {
            return $"{this.item.itemName}-event";
        }

        public virtual void setEventInactive()
        {
            this.isActive = false;
        }

        public virtual void setEventActive()
        {
            this.isActive = true;
        }

        public virtual bool active()
        {
            return this.isActive;
        }

        public virtual List<EventLookupInfo> contingentEvents()
        {
            return this.dependentEvents;
        }

        public virtual void execute()
        {
            player p = GameState.getGameState().playerReference;
            Debug.unityLogger.Log($" {p.name}");
            Debug.unityLogger.Log($" {item}");
            p.addToInventory(this.item);
        }
    }
}
