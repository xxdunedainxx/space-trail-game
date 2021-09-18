using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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
    private float xdest = 0;
    private float ydest = 0;
    private int waitSeconds = 0;
    private Vector2 movementSpeed;
    private Action endMovementCallBack;
    private Func<string> dependentMovementCheck;
    public GameObject objectToControl;
    private string currentMovementType;
    public string dependentMovementFlag;


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
                || currentMovementType == MovementTypes.DEPENDENT_MOVEMENT && this.dependentMovementFlag == MovementFlags.END)
            {
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
        float randX = UnityEngine.Random.RandomRange(0, maxX);
        float randY = UnityEngine.Random.RandomRange(0, maxY);

        float randXVector = UnityEngine.Random.RandomRange(0, randX);
        float randYVector = UnityEngine.Random.RandomRange(0, randY);

        Vector2 movementSpeed = new Vector2(randXVector, randYVector);

        Debug.unityLogger.Log($"Randomized move generated x {randX}, and y {randY}, from x {randX}, and y {randY}");

        StartCoroutine(this.Move(randX, randY, movementSpeed, endMovementCallBack, waitSeconds));
    }
}
