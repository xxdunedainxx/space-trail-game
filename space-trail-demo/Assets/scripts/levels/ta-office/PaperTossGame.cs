using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Assets.scripts.core;
//https://www.youtube.com/watch?v=wkKsl1Mfp5M <--- good ex of projectiles 

public class PaperTossGame : MonoBehaviour
{
    private bool holdingDownKey = false;
    private float _force = 0f;
    private float baseForce = 20f;
    private GameObject _paper;
    private Stopwatch sw;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.unityLogger.Log("Paper toss game loaded!");
        this._paper = GameObject.Find("paper-toss");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            UnityEngine.Debug.unityLogger.Log("Loading up paper toss..");
            this.sw = new Stopwatch();
            this.sw.Start();
        } else if (Input.GetKeyUp(KeyCode.T))
        {
            player p = GameState.getGameState().playerReference;
            this.sw.Stop();
            
            UnityEngine.Debug.unityLogger.Log("they tossed?");
            GameObject obj = Instantiate(this._paper);
            obj.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.GenerateVector(ref obj);
            this.holdingDownKey = false;
        }
    }

    void GenerateVector(ref GameObject obj)
    {
        float forceToApply = ((sw.ElapsedMilliseconds / 1000) * 50f);
        UnityEngine.Debug.unityLogger.Log($"Force to apply: {forceToApply}");
        player p = GameState.getGameState().playerReference;
        if(p.direction == Directions.EAST)
        {
            obj.gameObject.transform.position = new Vector3(p.transform.position.x + .25f, p.transform.position.y, 0);
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceToApply, forceToApply));
        }
        else if (p.direction == Directions.WEST)
        {
            obj.gameObject.transform.position = new Vector3(p.transform.position.x + -.25f, p.transform.position.y, 0);
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-forceToApply, forceToApply));
        } 
        else if(p.direction == Directions.SOUTH)
        {
            obj.gameObject.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + -.25f, 0);
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -forceToApply));
        }
        else
        {
            // north
            obj.gameObject.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + .25f, 0);
            obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, forceToApply));
        }
    }

    void TossPaper()
    {
        
    }
}
