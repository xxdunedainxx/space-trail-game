using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.core.events
{
    class EventListener
    {
        void consumeEvent(IEvent even)
        {
            even.execute();
            even.setEventInactive();
        }
    }
}
