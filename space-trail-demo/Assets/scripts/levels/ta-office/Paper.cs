using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    private player p;
    private Rigidbody2D rb;
    private bool stopped = false;

    // Start is called before the first frame update
    void Start()
    {
        this.p = GameState.getGameState().playerReference;
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(this.gameObject.transform.position.y - this.p.feet.transform.position.y) < .005f && stopped == false)
        {
            Debug.unityLogger.Log("remove gravity from paper");
            this.rb.velocity = Vector2.zero;
            this.rb.angularDrag = 0f;
            this.rb.angularVelocity = 0f;
            this.rb.isKinematic = true;
            stopped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "simple-open-blue-trashcan")
        {
            Debug.unityLogger.Log("they got it in the trashcan!");
            Destroy(this.gameObject);
        }
    }
}
