using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core;
using Assets.scripts.core.gameplay;
using Assets.scripts.core.dialogue;


namespace Assets.scripts.LoadingScreens.Chapters
{
    class Chapter1 : GameLoader
    {
        private void Start()
        {
            Debug.unityLogger.Log("chapter 1...");
            Persistence.playerRequired = false;
            this.Initialize();
            StartCoroutine(WaitForEffect());
           
        }

        private IEnumerator WaitForEffect()
        {
            yield return new WaitForSeconds(3);
            Level.levelTransition(LevelFactory.LECTURE_HALL);
        }
    }
}
