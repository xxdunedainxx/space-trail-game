using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class somebutton : MonoBehaviour
{
    public void SetText(string text)
    {
        Debug.unityLogger.Log("somebutton called");
        Text t = transform.Find("Text").GetComponent<Text>();
        t.text = text;
    }
}
