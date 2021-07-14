using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.core
{
    class Directions
    {
        public static string NORTH = "NORTH";
        public static string EAST = "EAST";
        public static string WEST = "WEST";
        public static string SOUTH = "SOUTH";
        public static int NORTH_MULTIPLYER = 1;
        public static int SOUTH_MULTIPLYER = -1;
        public static int EAST_MULTIPLYER = 1;
        public static int WEST_MULTIPLYER = -1;
        public static float NORTH_TRANSFORM = 0f;
        public static float SOUTH_TRANSFORM = 180f;
        public static float EAST_TRANSFORM = 0f;
        public static float WEST_TRANSFORM = 180f;
        public static Dictionary<string, int> DIRECTIONS_VALUE_MAP = new Dictionary<string, int>
        {
            {Directions.NORTH , Directions.NORTH_MULTIPLYER },
            {Directions.SOUTH, Directions.SOUTH_MULTIPLYER },
            {Directions.WEST, Directions.WEST_MULTIPLYER },
            {Directions.EAST, Directions.EAST_MULTIPLYER }
        };
        public static Dictionary<string, float> DIRECTIONS_TRANSFORM_MAP = new Dictionary<string, float>
        {
            {Directions.NORTH , Directions.NORTH_TRANSFORM },
            {Directions.SOUTH, Directions.SOUTH_TRANSFORM },
            {Directions.WEST, Directions.EAST_TRANSFORM },
            {Directions.EAST, Directions.WEST_TRANSFORM }
        };
    }
}
