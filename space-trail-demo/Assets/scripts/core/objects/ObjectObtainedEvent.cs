using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.core.objects
{
    class ObjectObtainedEvent : IEvent
    {
        public bool obtained;
        public SingleItem item;

        public ObjectObtainedEvent(bool obtained, SingleItem item)
        {
            this.obtained = obtained;
            this.item = item;
        }
    }
}
