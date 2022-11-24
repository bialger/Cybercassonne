using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject A;
    public GameObject MovingManager;
    private GameObject[] Tiles;
    private MotionManager _motionManager;
    private int counterTiles;
    private bool isChoosing;
    private float height;
    private float edge;

    // Start is called before the first frame update
    void Start()
    {
        _motionManager = MovingManager.GetComponent<MotionManager>();
        Tiles = new GameObject[72];
        counterTiles = 0;
        height = 0.2f;
        edge = 1.0f;
        isChoosing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isChoosing && !_motionManager.IsMoving())
            {
                AddTile(true);
            }
            else if (!_motionManager.IsRotating() && !_motionManager.IsMoving())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool make = Physics.Raycast(ray, out hit);
                if (make)
                {
                    if (Tiles[counterTiles - 1].transform.CompareTag(hit.transform.tag))
                    {
                        isChoosing = false;
                        _motionManager.StartMoving(0.0f, -height, 0.0f, 0.1f);
                    }
                    else
                    {
                        AddTile(false);
                    }
                }
            }
        }
    }

    //This method adds new tile
    private void AddTile(bool isNew)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool make = Physics.Raycast(ray, out hit); // it is not clear why, but in direct way variables, changed inside "if", do not change
        if (make)
        {
            if (hit.transform.tag == "GameController") // Later here should be prefab-plate tag
            {
                isChoosing = true;
                if (isNew)
                {
                    counterTiles++;
                }
                else
                {
                    Destroy(Tiles[counterTiles - 1]);
                }
                Tiles[counterTiles - 1] = AddPrefab(A, hit.point.x, height, hit.point.z); // TODO: from point to squares
                Tiles[counterTiles - 1].name += counterTiles;
                _motionManager.AssignRotationTarget(Tiles[counterTiles - 1]);
                _motionManager.AssignMotionTarget(Tiles[counterTiles - 1]);
            }
        }
    }

    // This method adds a given prefab and returns an GameObject
    private GameObject AddPrefab(GameObject prefab, float x, float y, float z)
    {
        return Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
    }

    // Some getters and setters
    public bool IsChoosing()
    {
        return isChoosing;
    }

    public void SetChoosing(bool choosing)
    {
        this.isChoosing = choosing;
    }
}
