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
    private float heightChoice;

    // Start is called before the first frame update
    void Start()
    {
        _motionManager = MovingManager.GetComponent<MotionManager>();
        Tiles = new GameObject[72];
        counterTiles = 0;
        heightChoice = 0.2f;
        isChoosing = false;
    }

    // Update is called once per frame
    void Update()
    {
        Transform CurrentTileTransform = (counterTiles != 0) ? Tiles[counterTiles - 1].transform : null;
        bool isNeitherMovingOrRotating = !_motionManager.IsMoving(CurrentTileTransform) && !_motionManager.IsRotating(CurrentTileTransform);
        if (Input.GetMouseButtonDown(0))
        {
            if (!isChoosing && isNeitherMovingOrRotating)
            {
                AddTile(true);
            }
            else if (isNeitherMovingOrRotating)
            {
                RaycastHit hit = CameraHit();
                if (hit.transform != null)
                {
                    if (Tiles[counterTiles - 1].transform.CompareTag(hit.transform.tag))
                    {
                        isChoosing = false;
                        _motionManager.StartMoving(CurrentTileTransform, 0.0f, -heightChoice, 0.0f, 0.1f);
                    }
                    else
                    {
                        AddTile(false);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (isChoosing && isNeitherMovingOrRotating)
            {
                _motionManager.StartRotating(CurrentTileTransform, 0.0f, 90.0f, 0.0f, 0.5f);
            }
        }
    }

    //This method adds new tile
    private void AddTile(bool isNew)
    {
        RaycastHit hit = CameraHit();
        if (hit.transform != null)
        {
            if (hit.transform.tag == "GameController") // Later here should be prefab-plate tag
            {
                isChoosing = true;
                if (isNew)
                {
                    counterTiles++;
                    Tiles[counterTiles - 1] = AddPrefab(A, hit.point.x, heightChoice, hit.point.z); // TODO: from point to squares
                    Tiles[counterTiles - 1].name += counterTiles;
                }
                else
                {
                    Tiles[counterTiles - 1].transform.position = new Vector3(hit.point.x, heightChoice, hit.point.z);
                }
            }
        }
    }

    // This method adds a given prefab and returns an GameObject
    private GameObject AddPrefab(GameObject prefab, float x, float y, float z)
    {
        return Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
    }

    private RaycastHit CameraHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
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
