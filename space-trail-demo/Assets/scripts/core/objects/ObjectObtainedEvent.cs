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
        public string name;

        public ObjectObtainedEvent(bool obtained, string name)
        {
            this.obtained = obtained;
            this.name = name;
        }
    }
}
