using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.core
{
    public interface IEventEmitter
    {
        void emitEvent(IEvent even);
        void addConsumer(IEventConsumer consumer);
    }
}
