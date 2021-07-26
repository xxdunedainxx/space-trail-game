using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Paper : MonoBehaviour
{
    private player p;
    private Rigidbody2D rb;
    private bool stopped = false;
    private Stopwatch sw;
    private long expirationTimeSeconds = 1;
    public PaperTossGame parentGame;
    [SerializeField]
    public bool primaryCopy = false;


    // Start is called before the first frame update
    void Start()
    {
        this.p = GameState.getGameState().playerReference;
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
        this.sw = new Stopwatch();
        this.sw.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(this.gameObject.transform.position.y - this.p.feet.transform.position.y) < .005f && stopped == false)
        {
            UnityEngine.Debug.unityLogger.Log("remove gravity from paper");
            this.StopObject();
        }
        else
        {
            this.EvaluateExpiration();
        }
    }

    private void StopObject()
    {
        this.rb.velocity = Vector2.zero;
        this.rb.angularDrag = 0f;
        this.rb.angularVelocity = 0f;
        this.rb.isKinematic = true;
        stopped = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (this.primaryCopy == false)
        {
            if (other.gameObject.name == "simple-open-blue-trashcan")
            {
                UnityEngine.Debug.unityLogger.Log("they got it in the trashcan!");
                this.parentGame.successfullTosses += 1;
                this.RemovePaper();
            }
        }
    }

    private void RemovePaper()
    {
        this.parentGame.attempts += 1;
        Destroy(this.gameObject);
    }

    private void EvaluateExpiration()
    {
        if((this.sw.ElapsedMilliseconds / 1000) > this.expirationTimeSeconds && primaryCopy == false)
        {
            UnityEngine.Debug.unityLogger.Log("paper expired");
            this.sw.Stop();
            this.RemovePaper();
        }
       
    }
}
