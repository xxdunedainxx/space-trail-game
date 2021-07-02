using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.scripts.levels.lecturehall;

namespace Assets.scripts.core.gameplay
{
    class LevelFactory
    {
        static string LECTURE_HALL = "LECTUREHALL";
        static string HALLWAY = "HALLWAY";

        static Dictionary<string, Func<Level>> LEVELS = new Dictionary<string, Func<Level>>
        {
            {LevelFactory.HALLWAY, generateHallway },
            {LevelFactory.LECTURE_HALL, generateLectureHall}
        };

        public static Level FetchLevel(string name)
        {
            return LevelFactory.LEVELS[name]();
        }

        private static Hallway generateHallway()
        {
            return new Hallway();
        }

        private static LectureHall generateLectureHall()
        {
            return new LectureHall();
        }
    }
}
