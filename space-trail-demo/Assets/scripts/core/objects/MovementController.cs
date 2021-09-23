using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.scripts.core;
using Assets.scripts.core.events;

public class MovementController : MonoBehaviour
{
    public static class MovementTypes
    {
        public static string DEPENDENT_MOVEMENT = "DEPT";
        public static string DISTANCE_MOVEMENT = "DIST";
    }

    public static class MovementFlags
    {
        public static string STOP = "stop";
        public static string END = "end";
        public static string START = "start";
        public static string CHANGE_SPEED = "CHANGE_SPEED";
        public static string NEUTRAL = "neutral";
    }

    private bool isMoving = false;
    private float movementStartLocationX = 0;
    private float movementStartLocationY = 0;
    public float xdest = 0;
    public float ydest = 0;
    private int waitSeconds = 0;
    private Vector2 movementSpeed = Vector2.zero;
    private Action endMovementCallBack;
    private Func<string> dependentMovementCheck;
    public GameObject objectToControl;
    private string currentMovementType;
    public string dependentMovementFlag;
    public float xBoundHigh = 0;
    public float xBoundLow = 0;
    public List<float> yBound = null;
    public int LayerBound = -1;
    private bool boundsCollisionDetected = false;

    public bool CheckXBound()
    {
        if(xBoundHigh == 0 || xBoundLow == 0)
        {
            return true;
        } else if(this.objectToControl.gameObject.transform.position.x > this.xBoundHigh || this.objectToControl.gameObject.transform.position.x < this.xBoundLow)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CheckYBound()
    {
        if (yBound == null)
        {
            return true;
        }
        else
        {
            foreach (float yB in this.yBound)
            {
                if (Math.Abs(this.objectToControl.gameObject.transform.position.y) > yB)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void ColliderCheckAndResetTrajectory(Collider2D collision)
    {
        if (this.LayerBound != -1 && collision.gameObject.layer == this.LayerBound)
        {
            this.boundsCollisionDetected = true;
            this.xdest = -this.xdest;
            this.ydest = -this.ydest;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        this.ColliderCheckAndResetTrajectory(collision.collider);
    }

    /*public void OnCollisionStay2D(Collision2D collision)
    {
        this.ColliderCheckAndResetTrajectory(collision.collider);
    }*/

    public void OnTriggerEnter2D(Collider2D collision)
    {
        this.ColliderCheckAndResetTrajectory(collision);
    }

    /*public void OnTriggerStay2D(Collider2D collision)
    {
        this.ColliderCheckAndResetTrajectory(collision);
    }*/

    public bool InBounds()
    {
        return ( boundsCollisionDetected == false); //CheckXBound() || CheckYBound() ||
    }

    public void SetMovementSpeed(Vector2 nSpeed)
    {
        this.movementSpeed = nSpeed;
    }

    private void CheckDependentMovement()
    {
        this.dependentMovementFlag = this.dependentMovementCheck();

        if(this.dependentMovementFlag == MovementFlags.CHANGE_SPEED)
        {
            this.MoveRigidBody();
        } else if(this.dependentMovementFlag == MovementFlags.STOP)
        {
            this.StopRigidBody();
        } else
        {
            return;
        }
    }

    private bool CheckMovementDistance()
    {
        //Debug.unityLogger.Log($"Check move distance: {Mathf.Abs(this.movementStartLocationX) - Mathf.Abs(this.transform.position.x)}, {Mathf.Abs(this.movementStartLocationX)}, {Mathf.Abs(this.transform.position.x)}, {Math.Abs(this.xdest)}");
        if ((Math.Abs(Mathf.Abs(this.movementStartLocationX) - Mathf.Abs(this.transform.position.x)) >= Math.Abs(this.xdest)) && (Math.Abs(Mathf.Abs(this.movementStartLocationY) - Mathf.Abs(this.transform.position.y)) >= Math.Abs(this.ydest)))
        {
            return true;
        }
        return false;
    }

    private void MoveRigidBody()
    {
        Debug.unityLogger.Log("moving rigid body");
        this.objectToControl.GetComponent<Rigidbody2D>().velocity = this.movementSpeed;
    }

    private void StopRigidBody()
    {
        Debug.unityLogger.Log("stop rigid body...");
        this.objectToControl.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if ((currentMovementType == MovementTypes.DISTANCE_MOVEMENT && CheckMovementDistance())
                || currentMovementType == MovementTypes.DEPENDENT_MOVEMENT && this.dependentMovementFlag == MovementFlags.END
                || InBounds() == false
            )
            {
                this.boundsCollisionDetected = false;
                Debug.unityLogger.Log("THEY REACHED THE DISTANCE");
                this.isMoving = false;
                this.StopRigidBody();
                if (this.endMovementCallBack != null)
                {
                    this.endMovementCallBack();
                }
            } else if(currentMovementType == MovementTypes.DEPENDENT_MOVEMENT)
            {
                this.CheckDependentMovement();
            }
        }
    }

    public IEnumerator Move(Vector2 movementSpeed, Func<string> deptFunction,Action endMovementCallBack = null, int waitSeconds = 0)
    {
        yield return new WaitForSeconds(waitSeconds);
        this.dependentMovementCheck = deptFunction;
        this.currentMovementType = MovementTypes.DEPENDENT_MOVEMENT;
        this.dependentMovementFlag = MovementFlags.START;
        this.waitSeconds = waitSeconds;
        this.isMoving = true;
        this.movementStartLocationX = this.transform.position.x;
        this.movementStartLocationY = this.transform.position.y;
        this.movementSpeed = movementSpeed;
        this.MoveRigidBody();
        this.endMovementCallBack = endMovementCallBack;
    }

    public IEnumerator Move(float xdestination, float ydestination, Vector2 movementSpeed, Action endMovementCallBack = null, int waitSeconds = 0)
    {
        yield return new WaitForSeconds(waitSeconds);
        this.currentMovementType = MovementTypes.DISTANCE_MOVEMENT;
        this.waitSeconds = waitSeconds;
        this.isMoving = true;
        this.movementStartLocationX = this.transform.position.x;
        this.movementStartLocationY = this.transform.position.y;
        this.xdest = xdestination;
        this.ydest = ydestination;
        this.movementSpeed = movementSpeed;
        this.MoveRigidBody();
        this.endMovementCallBack = endMovementCallBack;
    }

    public void RandomizedMove(float maxX, float maxY, Action endMovementCallBack = null, int waitSeconds = 0)
    {
        float randX = maxX != 0 ? UnityEngine.Random.RandomRange(-maxX, maxX) : 0;
        float randY = maxY != 0 ? UnityEngine.Random.RandomRange(-maxY, maxY): 0;
        if (this.movementSpeed.Equals(Vector2.zero))
        {
            float randXVector = maxX != 0 ? UnityEngine.Random.RandomRange(0, randX) : 0;
            float randYVector = maxY != 0 ? UnityEngine.Random.RandomRange(0, randY) : 0;

            this.movementSpeed = new Vector2(randXVector, randYVector);
        }
        Debug.unityLogger.Log($"Randomized move generated x {randX}, and y {randY}, from x {randX}, and y {randY}");

        StartCoroutine(this.Move(randX, randY, this.movementSpeed, endMovementCallBack, waitSeconds));
    }
}
