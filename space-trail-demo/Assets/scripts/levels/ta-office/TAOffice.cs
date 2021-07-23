using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.objects;


public class TAOffice : Level
{
    private SpacetrailGame _game;
    public TAOffice() : base("TAOffice", false)
    {
        Debug.unityLogger.Log("TA Office constructor");
        this._game = GameObject.Find("TA-Office").GetComponent<SpacetrailGame>();
        this._game.gameObject.AddComponent<PaperTossGame>();
    }
}