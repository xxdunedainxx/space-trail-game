using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Assets.scripts.core.objects
{
    [Serializable]
    public class LabKit : BasicItem
    {
        public static LabKitComponent MICROSCOPE = new LabKitComponent("microscope");
        public List<LabKitComponent> components = null;

        public LabKit(string labKitName)
        {
            this.itemName = labKitName;
        }

        public LabKit(string labKitName, List<LabKitComponent> components)
        {
            this.itemName = labKitName;
            this.components = components;
        }

        public string Description()
        {
            string componentStringBuilder = "";
            foreach(LabKitComponent c  in this.components)
            {
                componentStringBuilder += $"\nLab Kit Component: {c.name}";
            }
            return $"{this.name()} - {componentStringBuilder}";
        }
    }

    [Serializable]
    public class LabKitComponent
    {
        public string name = "microscope";
        public LabKitComponent(string name)
        {
            this.name = name;
        }

        public LabKitComponent()
        {

        }
    }
}
