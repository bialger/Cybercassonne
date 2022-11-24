using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionManager : MonoBehaviour
{
    private GameManager _gameManager;
    private GameObject RotateThis;
    private bool isRotating;
    private float startRotationTime;
    private float rotationTime;
    private Quaternion startAngle;
    private Quaternion targetAngle;
    private GameObject MoveThis;
    private bool isMoving;
    private float startMotionTime;
    private float motionTime;
    private Vector3 startPosition;
    private Vector3 deltaPosition;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isRotating = false;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateIteration();
        MovingIteration();
        if (Input.GetMouseButtonDown(1))
        {
            if (_gameManager.IsChoosing())
            {
                StartRotating(0.0f, 90.0f, 0.0f, 0.5f);
            }
        }
    }

    // This method begins rotation of an object for a given time (seconds)
    public void StartRotating(float angleX, float angleY, float angleZ, float time)
    {
        if (!isRotating)
        {
            isRotating = true;
            rotationTime = time;
            try
            {
                startAngle = RotateThis.transform.rotation;
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("Ignore this error in rotating: " + ex.ToString());
            }
            targetAngle = Quaternion.Euler(startAngle.eulerAngles.x + angleX, startAngle.eulerAngles.y + angleY, startAngle.eulerAngles.z + angleZ);
            startRotationTime = Time.time;
        }
    }

    // This method begins motion of a given an object for a given time (seconds)
    public void StartMoving(float deltaX, float deltaY, float deltaZ, float time)
    {
        if (!isMoving)
        {
            isMoving = true;
            motionTime = time;
            startPosition = MoveThis.transform.position;
            deltaPosition = new Vector3(deltaX, deltaY, deltaZ);
            startMotionTime = Time.time;
        }
    }

    // This method continues rotation
    private void RotateIteration()
    {
        if (isRotating)
        {
            if (Time.time - startRotationTime < rotationTime)
            {
                try // I dont really know why, but this works although NRE occurs.
                {
                    RotateThis.transform.rotation = Quaternion.Lerp(startAngle, targetAngle, (Time.time - startRotationTime) / rotationTime);
                }
                catch (NullReferenceException ex)
                {
                    Debug.Log("Ignore this error in rotating: " + ex.ToString());
                }
            }
            else
            {
                isRotating = false;
                try
                {
                    RotateThis.transform.rotation = targetAngle;
                }
                catch (NullReferenceException ex)
                {
                    Debug.Log("Ignore this error in rotating: " + ex.ToString());
                }
            }
        }
    }

    // This method continues motion
    private void MovingIteration()
    {
        if (isMoving)
        {
            if (Time.time - startMotionTime < motionTime)
            {
                MoveThis.transform.position = startPosition + deltaPosition * (Time.time - startMotionTime) / motionTime;
            }
            else
            {
                isMoving = false;
                MoveThis.transform.position = startPosition + deltaPosition;
            }
        }
    }

    // Some setters and getters
    public void AssignRotationTarget(GameObject rotationTarget)
    {
        this.RotateThis = rotationTarget;
    }

    public void AssignMotionTarget(GameObject motionTarget)
    {
        this.MoveThis = motionTarget;
    }

    public bool IsRotating()
    {
        return isRotating;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
