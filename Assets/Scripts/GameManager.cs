using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject A;
    private GameObject[] PlatePrefabs;
    private GameObject RotateThis;
    private int plates;
    private bool rotation;
    private bool choosing;
    private float height;
    private float edge;
    private float startTime;
    private float rotationTime;
    private Quaternion startAngle;
    private Quaternion targetAngle;

    // Start is called before the first frame update
    void Start()
    {
        PlatePrefabs = new GameObject[72];
        plates = 0;
        height = 0.2f;
        edge = 1.0f;
        rotation = false;
        choosing = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateIteration();
        if (Input.GetMouseButtonDown(0))
        {
            if (!choosing)
            {
                addCell(true);
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool make = Physics.Raycast(ray, out hit);
                if (make)
                {
                    if (hit.transform.tag == PlatePrefabs[plates - 1].transform.tag)
                    {
                        choosing = false;
                        MoveObject(PlatePrefabs[plates - 1], 0.0f, -height, 0.0f, 1.0f);
                    }
                    else
                    {
                        addCell(false);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (choosing)
            {
                StartRotating(PlatePrefabs[plates - 1], 0.0f, 90.0f, 0.0f, 0.5f);
            }
        }
    }

    //this method casts new 
    private void addCell(bool isNew)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool make = Physics.Raycast(ray, out hit); // it is not clear why, but in direct way variables, changed inside "if", do not change
        if (make)
        {
            if (hit.transform.tag == "GameController") // Later here should be prefab-plate tag
            {
                choosing = true;
                if (isNew)
                {
                    plates++;
                }
                else
                {
                    Destroy(PlatePrefabs[plates - 1]);
                }
                PlatePrefabs[plates - 1] = AddPrefab(A, hit.point.x, height, hit.point.z); // TODO: from point to squares
            }
        }
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

    // this method adds a given prefab and returns an GameObject
    private GameObject AddPrefab(GameObject prefab, float x, float y, float z)
    {
        return Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
    }
}
