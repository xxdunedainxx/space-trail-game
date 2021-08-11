using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.core.events
{
    class EventToggle : IEvent
    {
        private string eventName;
        private bool isActive;
        List<EventLookupInfo> dependentEvents;

        public EventToggle(string name)
        {
            this.eventName = name;
        }

        public bool active()
        {
            return this.isActive;
        }

        public List<EventLookupInfo> contingentEvents()
        {
            return this.dependentEvents;
        }

        public virtual void ToggleOn()
        {
            // can potentially override
            foreach(EventLookupInfo ev in this.dependentEvents)
            {
                //EventSubscriptionFactory.instance.
                //ev.setEventActive();
            }
        }

        public virtual void ToggleOff()
        {
            // can potentially override
            foreach (EventLookupInfo ev in this.dependentEvents)
            {
                //ev.setEventInactive();
            }
        }

        public void execute()
        {
            
        }

        public string name()
        {
            return this.eventName;
        }

        public void setEventActive()
        {
            this.isActive = true;
        }

        public void setEventInactive()
        {
            this.isActive = false;
        }
    }
}
