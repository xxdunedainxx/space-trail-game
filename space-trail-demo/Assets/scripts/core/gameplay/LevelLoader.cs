using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.SetInstance();
        }
    }

    private void SetInstance()
    {
        instance = this;
    }

    IEnumerator DelayTransition(string toLevel)
    {
        yield return new WaitForSeconds(.75f);   //(LevelTransitionHandler.instance.transitionAnimator.runtimeAnimatorController.animationClips[0].averageDuration);
        this.transition(toLevel);
    }

    private void transition(string toLevel)
    {
        Persistence.PersistData();
        GameState.getGameState().notReady();
        SceneManager.LoadScene(toLevel);
    }

    public void levelTransition(string toLevel)
    {
        LevelTransitionHandler transition = LevelTransitionHandler.instance;
        transition.Transition();
        StartCoroutine(DelayTransition(toLevel));
    }
}
