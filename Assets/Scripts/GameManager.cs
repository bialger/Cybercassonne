using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject RotateThis;
    private int plates;
    private Boolean rotation;
    private float startTime;
    private float rotationTime;
    private Quaternion startAngle;
    private Quaternion targetAngle;

    // Start is called before the first frame update
    void Start()
    {
        rotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this method adds a given prefab and returns an GameObject
    private GameObject AddPrefab(GameObject prefab, float x, float y, float z) 
    {
        return Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
    }

    // this method continues rotation
    private void RotateIteration() 
    {
        if (rotation)
        {
            if (Time.time - startTime < rotationTime)
            {
                RotateThis.transform.rotation = Quaternion.Lerp(startAngle, targetAngle, (Time.time - startTime) / rotationTime);
            }
            else
            {
                rotation = false;
                RotateThis.transform.rotation = targetAngle;
            }
        }
    }

    // this method begins rotation of a given object for a given time (seconds)
    private void StartRotating(GameObject Obj, float angleX, float angleY, float angleZ, float time) 
    {
        if (!rotation)
        {
            RotateThis = Obj;
            rotation = true;
            rotationTime = time;
            startAngle = RotateThis.transform.rotation;
            targetAngle = Quaternion.Euler(startAngle.eulerAngles.x + angleX, startAngle.eulerAngles.y + angleY, startAngle.eulerAngles.z + angleZ);
            startTime = Time.time;
        }
    }

    // this method rotates a given object instantly
    private void RotateObject(GameObject Obj, float angleX, float angleY, float angleZ) 
    {
        Obj.transform.Rotate(angleX, angleY, angleZ);
    }

    // this method moves a given object at a given speed for a given time
    private void MoveObject(GameObject Obj, float velX, float velY, float velZ, float deltaTime) 
    {
        Obj.transform.position += new Vector3(velX, velY, velZ) * deltaTime;
    }
}
