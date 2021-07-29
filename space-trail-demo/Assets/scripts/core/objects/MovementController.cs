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
    private Vector2 movementSpeed;
    private Action endMovementCallBack;
    public GameObject objectToControl;


    private bool CheckMovementDistance()
    {
        //Debug.unityLogger.Log($"{Mathf.Abs(this.movementStartLocationX) - Mathf.Abs(this.transform.position.x)}, {Mathf.Abs(this.movementStartLocationX)}, {Mathf.Abs(this.transform.position.x)}");
        if (Math.Abs(Mathf.Abs(this.movementStartLocationX) - Mathf.Abs(this.transform.position.x)) >= Math.Abs(this.xdest))
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
        this.isMoving = true;
        this.movementStartLocationX = this.transform.position.x;
        this.movementStartLocationY = this.transform.position.y;
        this.xdest = xdestination;
        this.ydest = ydestination;
        this.movementSpeed = movementSpeed;
        this.objectToControl.GetComponent<Rigidbody2D>().velocity = movementSpeed;
        this.endMovementCallBack = endMovementCallBack;
    }
}
