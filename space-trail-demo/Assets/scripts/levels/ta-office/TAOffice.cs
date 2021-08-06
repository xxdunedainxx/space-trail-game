using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.objects;
using Assets.scripts.ui;


public class TAOffice : Level
{
    private SpacetrailGame _game;
    private AnimationCollisionTrigger catCollisionTrigger;
    public TAOffice() : base("TAOffice", false)
    {
        Debug.unityLogger.Log("TA Office constructor");
        this._game = GameObject.Find("TA-Office").GetComponent<SpacetrailGame>();
        this._game.gameObject.AddComponent<PaperTossGame>();
        GameObject.Find("catCollisionTrigger").AddComponent<AnimationCollisionTrigger>();
        this.catCollisionTrigger = GameObject.Find("catCollisionTrigger").GetComponent<AnimationCollisionTrigger>();
        GameObject.Find("Tabby_Cat").GetComponent<Animator>().enabled = false;
        this.catCollisionTrigger.gameObjectToAnimate = "Tabby_Cat";
        this.catCollisionTrigger.animatorToPlay = "Cat_Angry_Clip";
    }
}