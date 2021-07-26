using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.objects;

namespace Assets.scripts.levels.outside_college_area {
    public class OutsideLecturehall : Level
    {
        public OutsideLecturehall() : base("OutsideLectureHall", false)
        {
            Debug.unityLogger.Log("OutsideLectureHall constructor");
        }
    }
}
