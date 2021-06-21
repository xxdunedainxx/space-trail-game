using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core
{
    public interface Dependency<ArgTypeInjected>
    {
        void updateDependency(ArgTypeInjected arg);
        ArgTypeInjected value();
        string name();
    }
}
