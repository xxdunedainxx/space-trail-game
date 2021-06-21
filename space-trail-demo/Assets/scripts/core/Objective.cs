using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core
{
    public class Objective : MonoBehaviour
    {
        [SerializeField]
        public string objective;
        [SerializeField]
        public Dictionary<string, Dependency<bool>> dependencies;
        private bool complete = false;

        public Objective(string objectiveDescription, Dictionary<string, Dependency<bool>> dependencies)
        {
            this.objective = objectiveDescription;
            this.dependencies = dependencies;
        }

        private void checkCompletion()
        {
            foreach(string dep in this.dependencies.Keys)
            {
                if (this.dependencies[dep].value() == false)
                {
                    return;
                }
            }
            this.complete = true;
        }

        public bool isComplete()
        {
            return this.complete;
        }

        void disableDependency(string dependency)
        {
            this.dependencies[dependency].updateDependency(false);
            this.checkCompletion();
        }

        void enableDependency(string dependency)
        {
            this.dependencies[dependency].updateDependency(true);
        }
    }
}
