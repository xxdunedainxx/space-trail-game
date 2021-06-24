using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.core
{
    public interface IEvent
    {
        string name();
        bool active();
        void execute();
        void setEventInactive();
        void setEventActive();
        List<IEvent> contingentEvents();
    }
}
