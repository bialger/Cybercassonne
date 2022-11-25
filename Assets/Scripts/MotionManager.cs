using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionManager : MonoBehaviour
{
    private List<GameObject> RotationTargets;
    private List<bool> areRotating;
    private List<float> startRotationTimes;
    private List<float> rotationTimes;
    private List<Quaternion> startAngles;
    private List<Quaternion> targetAngles;
    private int countRotations;
    private List<GameObject> MotionTargets;
    private List<bool> areMoving;
    private List<float> startMotionTimes;
    private List<float> motionTimes;
    private List<Vector3> startPositions;
    private List<Vector3> deltaPositions;
    private int countMotions;

    // Start is called before the first frame update
    void Start()
    {
        RotationTargets = new List<GameObject>();
        areRotating = new List<bool>();
        startRotationTimes = new List<float>();
        rotationTimes = new List<float>();
        startRotationTimes = new List<float>();
        startAngles = new List<Quaternion>();
        targetAngles = new List<Quaternion>();
        MotionTargets = new List<GameObject>();
        areMoving = new List<bool>();
        startMotionTimes = new List<float>();
        motionTimes = new List<float>();
        startPositions = new List<Vector3>();
        deltaPositions = new List<Vector3>();
        countMotions = 0;
        countRotations = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RotateIteration();
        MovingIteration();
    }

    // This method begins rotation of an object for a given time (seconds)
    public void StartRotating(int index, float angleX, float angleY, float angleZ, float time)
    {
        if (!areRotating[index])
        {
            areRotating[index] = true;
            rotationTimes[index] = time;
            startAngles[index] = RotationTargets[index].transform.rotation;
            targetAngles[index] = Quaternion.Euler(startAngles[index].eulerAngles.x + angleX, startAngles[index].eulerAngles.y + angleY, startAngles[index].eulerAngles.z + angleZ);
            startRotationTimes[index] = Time.time;
        }
    }

    // This method begins motion of a given an object for a given time (seconds)
    public void StartMoving(int index, float deltaX, float deltaY, float deltaZ, float time)
    {
        if (!areMoving[index])
        {
            areMoving[index] = true;
            motionTimes[index] = time;
            startPositions[index] = MotionTargets[index].transform.position;
            deltaPositions[index] = new Vector3(deltaX, deltaY, deltaZ);
            startMotionTimes[index] = Time.time;
        }
    }

    // This method continues rotation
    private void RotateIteration()
    {
        for (int i = 0; i < countRotations; i++)
        {
            if (areRotating[i])
            {
                if (Time.time - startRotationTimes[i] < rotationTimes[i])
                {
                    RotationTargets[i].transform.rotation = Quaternion.Lerp(startAngles[i], targetAngles[i], (Time.time - startRotationTimes[i]) / rotationTimes[i]);
                }
                else
                {
                    areRotating[i] = false;
                    RotationTargets[i].transform.rotation = targetAngles[i];
                }
            }
        }
    }

    // This method continues motion
    private void MovingIteration()
    {
        for (int i = 0; i < countMotions; i++)
        {
            if (areMoving[i])
            {
                if (Time.time - startMotionTimes[i] < motionTimes[i])
                {
                    MotionTargets[i].transform.position = startPositions[i] + deltaPositions[i] * (Time.time - startMotionTimes[i]) / motionTimes[i];
                }
                else
                {
                    areMoving[i] = false;
                    MotionTargets[i].transform.position = startPositions[i] + deltaPositions[i];
                }
            }
        }
    }

    // Some setters and getters
    public void AssignRotationTarget(GameObject rotationTarget)
    {
        this.RotationTargets.Add(rotationTarget);
        areMoving.Add(false);
        rotationTimes.Add(0.0f);
        startAngles.Add(Quaternion.Euler(0, 0, 0));
        targetAngles.Add(Quaternion.Euler(0, 0, 0));
        startRotationTimes.Add(0.0f);
        countRotations++;
    }

    public void AssignMotionTarget(GameObject motionTarget)
    {
        this.MotionTargets.Add(motionTarget);
        areRotating.Add(false);
        motionTimes.Add(0.0f);
        startPositions.Add(new Vector3(0, 0, 0));
        deltaPositions.Add(new Vector3(0, 0, 0));
        startMotionTimes.Add(0.0f);
        countMotions++;
    }

    public bool IsRotating(int index)
    {
        if (index == -1)
        {
            return false;
        }
        else return areRotating[index];
    }

    public bool IsMoving(int index)
    {
        if (index == -1)
        {
            return false;
        }
        else return areMoving[index];
    }
}
