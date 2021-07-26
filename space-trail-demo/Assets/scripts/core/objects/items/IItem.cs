using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assets.scripts.core
{
    
    public interface IItem 
    {
        string spriteName();
        string name();
        string id();
        string description();
    }
}
