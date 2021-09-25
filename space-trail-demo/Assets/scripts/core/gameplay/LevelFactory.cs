using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.scripts.levels.lecturehall;
using Assets.scripts.levels.outside_college_area;
using Assets.scripts.levels;
using Assets.scripts.LoadingScreens.Chapters;
using Assets.scripts.levels.outside_college_area.overlook;

namespace Assets.scripts.core.gameplay
{
    class LevelFactory
    {
        public static string LECTURE_HALL = "LECTUREHALL";
        public static string HALLWAY = "HALLWAY";
        public static string TA_OFFICE = "TAOffice";
        public static string OUTSIDE_LECTUREHALL = "OutsideLectureHall";
        public static string TRANSITION_TO_CITY = "TransitionTocity";
        public static string CITY = "city-area";
        public static string LENNYS_BAR = "LennysBar";
        public static string CHAPTER_ONE = "Chapter1";
        public static string EIGHT_TWELVE = "inside-8-12";
        public static string SCHOOL_OVERLOOK = "school-overlook";

        static Dictionary<string, Func<Level>> LEVELS = new Dictionary<string, Func<Level>>
        {
            {LevelFactory.HALLWAY, generateHallway },
            {LevelFactory.LECTURE_HALL, generateLectureHall},
            {LevelFactory.TA_OFFICE, generateTAOffice },
            {LevelFactory.OUTSIDE_LECTUREHALL, generateOutsideLectureHall },
            {LevelFactory.TRANSITION_TO_CITY, generateTransitionToCity },
            {LevelFactory.CITY, generateCity },
            {LevelFactory.LENNYS_BAR, generateLennysBar },
            {LevelFactory.EIGHT_TWELVE,  generateEightTwelve},
            {LevelFactory.SCHOOL_OVERLOOK,  generateSchoolOverlook}
        };

        public static Level generateEightTwelve()
        {
            return new EightTwelve();
        }

        public static Level generateSchoolOverlook()
        {
            return new SchoolOverlook();
        }

        public static Level FetchLevel(string name)
        {
            return LevelFactory.LEVELS[name]();
        }

        private static Hallway generateHallway()
        {
            return new Hallway();
        }

        private static LevelLectureHall generateLectureHall()
        {
            return new LevelLectureHall();
        }

        private static TAOffice generateTAOffice()
        {
            return new TAOffice();
        }

        private static OutsideLecturehall generateOutsideLectureHall()
        {
            return new OutsideLecturehall();
        }

        private static TransitionTocity generateTransitionToCity()
        {
            return new TransitionTocity();
        }

        private static City generateCity()
        {
            return new City();
        }

        private static LennysBar generateLennysBar()
        {
            return new LennysBar();
        }
    }
}
