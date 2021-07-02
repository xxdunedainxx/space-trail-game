using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.scripts.core;
using Assets.scripts.core.objects;
using Assets.scripts.core.objects;

public class Hallway : Level
{
    public Hallway() : base("Hallway", true)
    {
        Debug.unityLogger.Log("hallway constructor");
    }
}