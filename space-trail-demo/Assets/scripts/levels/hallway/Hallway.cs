using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway : Level
{
    public Hallway() : base("Hallway", true)
    {
        Debug.unityLogger.Log("Hallway constructor");
    }

    void Start()
    {
        base.Start();
        Debug.unityLogger.Log("Hallway Level");
    }
}
