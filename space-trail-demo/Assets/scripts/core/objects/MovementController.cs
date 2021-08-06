using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MovementController : MonoBehaviour
{
    private bool isMoving = false;
    private float movementStartLocationX = 0;
    private float movementStartLocationY = 0;
    private float xdest = 0;
    private float ydest = 0;
    private int waitSeconds = 0;
    private Vector2 movementSpeed;
    private Action endMovementCallBack;
    public GameObject objectToControl;


    private bool CheckMovementDistance()
    {
        Debug.unityLogger.Log($"Check move distance: {Mathf.Abs(this.movementStartLocationX) - Mathf.Abs(this.transform.position.x)}, {Mathf.Abs(this.movementStartLocationX)}, {Mathf.Abs(this.transform.position.x)}, {Math.Abs(this.xdest)}");
        if ((Math.Abs(Mathf.Abs(this.movementStartLocationX) - Mathf.Abs(this.transform.position.x)) >= Math.Abs(this.xdest)) && (Math.Abs(Mathf.Abs(this.movementStartLocationY) - Mathf.Abs(this.transform.position.y)) >= Math.Abs(this.ydest)))
        {
            return true;
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (CheckMovementDistance())
            {
                Debug.unityLogger.Log("THEY REACHED THE DISTANCE");
                this.isMoving = false;
                this.objectToControl.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (this.endMovementCallBack != null)
                {
                    this.endMovementCallBack();
                }
            }
        }
    }

    public IEnumerator Move(float xdestination, float ydestination, Vector2 movementSpeed, Action endMovementCallBack = null, int waitSeconds = 0)
    {
        yield return new WaitForSeconds(waitSeconds);
        this.waitSeconds = waitSeconds;
        this.isMoving = true;
        this.movementStartLocationX = this.transform.position.x;
        this.movementStartLocationY = this.transform.position.y;
        this.xdest = xdestination;
        this.ydest = ydestination;
        this.movementSpeed = movementSpeed;
        this.objectToControl.GetComponent<Rigidbody2D>().velocity = movementSpeed;
        this.endMovementCallBack = endMovementCallBack;
    }

    public void RandomizedMove(float maxX, float maxY, Action endMovementCallBack = null, int waitSeconds = 0)
    {
        float randX = UnityEngine.Random.RandomRange(0, maxX);
        float randY = UnityEngine.Random.RandomRange(0, maxY);

        float randXVector = UnityEngine.Random.RandomRange(0, randX);
        float randYVector = UnityEngine.Random.RandomRange(0, randY);

        Vector2 movementSpeed = new Vector2(randXVector, randYVector);

        Debug.unityLogger.Log($"Randomized move generated x {randX}, and y {randY}, from x {randX}, and y {randY}");

        StartCoroutine(this.Move(randX, randY, movementSpeed, endMovementCallBack, waitSeconds));
    }
}
