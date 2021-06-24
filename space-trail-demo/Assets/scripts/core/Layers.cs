using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.core
{
    class Layers
    {
        public static LayerMask PLAYER_LAYER = LayerMask.GetMask("playerlayer");
        public static LayerMask DEFAUL_LAYER = LayerMask.GetMask("default");
        public static LayerMask BACKGROUND_IMAGE = LayerMask.GetMask("backgroundimage");
        public static int BACKGROUND_IMAGE_LAYER_VALUE = LayerMask.NameToLayer("backgroundimage");
    }
}
