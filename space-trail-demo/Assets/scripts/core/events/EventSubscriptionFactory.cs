using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.core.events
{
    class EventSubscriptionFactory
    {
        private Dictionary<string, Dictionary<string, IEvent>> _subs = new Dictionary<string, Dictionary<string, IEvent>>();
        public static EventSubscriptionFactory instance { get; private set; } = new EventSubscriptionFactory();

        private EventSubscriptionFactory()
        {
            this._subs["clickEvents"] = new Dictionary<string, IEvent>();
        }

        public void AddEvent(string eventType, string eventName, IEvent even)
        {
            if (this._subs.ContainsKey(eventType))
            {
                this._subs[eventType][eventName] = even;
            }
        }

        public void AddEvent(EventLookupInfo info, IEvent even)
        {
            if (this._subs.ContainsKey(info.eventType))
            {
                this._subs[info.eventType][info.eventName] = even;
            }
        }

        public IEvent GetEvent(EventLookupInfo info)
        {
            if (this._subs.ContainsKey(info.eventType) && this._subs[info.eventType].ContainsKey(info.eventName))
            {
                return this._subs[info.eventType][info.eventName];
            }
            else
            {
                return null;
            }
        }


        public void ExecuteEvent(string eventType, string eventName)
        {
            if(this._subs.ContainsKey(eventType) && this._subs[eventType].ContainsKey(eventName))
            {
                this._subs[eventType][eventName].execute();
                this._subs[eventType][eventName].setEventInactive();
            }
        }

        public void ExecuteEvent(EventLookupInfo info)
        {
            if (this._subs.ContainsKey(info.eventType) && this._subs[info.eventType].ContainsKey(info.eventName))
            {
                this._subs[info.eventType][info.eventName].execute();
                this._subs[info.eventType][info.eventName].setEventInactive();
            }
        }
    }

    public class EventLookupInfo
    {
        public string eventName;
        public string eventType;

        public EventLookupInfo(string name, string type)
        {
            this.eventName = name;
            this.eventType = type;
        }
    }
}
