using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionManager : MonoBehaviour
{
    private List<Transform> RotationTargets;
    private List<bool> areRotating;
    private List<float> startRotationTimes;
    private List<float> rotationTimes;
    private List<Quaternion> startAngles;
    private List<Quaternion> deltaAngles;
    private int countRotations;
    private List<Transform> MotionTargets;
    private List<bool> areMoving;
    private List<float> startMotionTimes;
    private List<float> motionTimes;
    private List<Vector3> startPositions;
    private List<Vector3> deltaPositions;
    private int countMotions;

    // Start is called before the first frame update
    void Start()
    {
        RotationTargets = new List<Transform>();
        areRotating = new List<bool>();
        startRotationTimes = new List<float>();
        rotationTimes = new List<float>();
        startRotationTimes = new List<float>();
        startAngles = new List<Quaternion>();
        deltaAngles = new List<Quaternion>();
        MotionTargets = new List<Transform>();
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
    public void StartRotating(Transform rotationTarget, float angleX, float angleY, float angleZ, float time)
    {
        RotationTargets.Add(rotationTarget);
        areRotating.Add(true);
        rotationTimes.Add(time);
        startAngles.Add(rotationTarget.transform.rotation);
        deltaAngles.Add(Quaternion.Euler(rotationTarget.transform.rotation.eulerAngles.x + angleX, rotationTarget.transform.rotation.eulerAngles.y + angleY, rotationTarget.transform.rotation.eulerAngles.z + angleZ));
        startRotationTimes.Add(Time.time);
        countRotations++;
    }

    // This method begins motion of a given an object for a given time (seconds)
    public void StartMoving(Transform motionTarget, float deltaX, float deltaY, float deltaZ, float time)
    {
        MotionTargets.Add(motionTarget);
        areMoving.Add(true);
        motionTimes.Add(time);
        startPositions.Add(motionTarget.transform.position);
        deltaPositions.Add(new Vector3(deltaX, deltaY, deltaZ));
        startMotionTimes.Add(Time.time);
        countMotions++;
    }

    // This method continues rotation
    private void RotateIteration()
    {
        List<int> endedIndices = new List<int>();
        for (int i = 0; i < countRotations; i++)
        {
            if (areRotating[i])
            {
                if (Time.time - startRotationTimes[i] < rotationTimes[i])
                {
                    RotationTargets[i].transform.rotation = Quaternion.Lerp(startAngles[i], deltaAngles[i], (Time.time - startRotationTimes[i]) / rotationTimes[i]);
                }
                else
                {
                    RotationTargets[i].transform.rotation = deltaAngles[i];
                    endedIndices.Add(i);
                }
            }
        }
        foreach (int i in endedIndices)
        {
            RotationTargets.RemoveAt(i);
            areRotating.RemoveAt(i);
            rotationTimes.RemoveAt(i);
            startAngles.RemoveAt(i);
            deltaAngles.RemoveAt(i);
            startRotationTimes.RemoveAt(i);
        }
        countRotations -= endedIndices.Count;
    }

    // This method continues motion
    private void MovingIteration()
    {
        List<int> endedIndices = new List<int>();
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
                    MotionTargets[i].transform.position = startPositions[i] + deltaPositions[i];
                    endedIndices.Add(i);
                }
            }
        }
        foreach (int i in endedIndices)
        {
            MotionTargets.RemoveAt(i);
            areMoving.RemoveAt(i);
            motionTimes.RemoveAt(i);
            startPositions.RemoveAt(i);
            deltaPositions.RemoveAt(i);
            startMotionTimes.RemoveAt(i);
        }
        countMotions -= endedIndices.Count;
    }

    // Some getters
    public bool IsRotating(Transform rotationTarget)
    {
        bool res = false;
        if (rotationTarget != null)
        {
            int index = MotionTargets.LastIndexOf(rotationTarget);
            if (index != -1)
                res = areMoving[index];
        }
        return res;
    }

    public bool IsMoving(Transform motionTarget)
    {
        bool res = false;
        if (motionTarget != null)
        {
            int index = MotionTargets.LastIndexOf(motionTarget);
            if (index != -1)
                res = areMoving[index];
        }
        return res;
    }
}
