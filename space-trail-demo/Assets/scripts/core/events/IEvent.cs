using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.scripts.core.events;

namespace Assets.scripts.core
{
    public interface IEvent
    {
        string name();
        bool active();
        void execute();
        void setEventInactive();
        void setEventActive();
        List<EventLookupInfo> contingentEvents();
    }
}
