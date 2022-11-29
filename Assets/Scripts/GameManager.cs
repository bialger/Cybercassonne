using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject MotionManager;
    private GameObject PrefabManager;
    private GameObject[] Tiles;
    private string[] tileNames;
    private string[] tiles;
    private MotionManager _motionManager;
    private PrefabManager _prefabManager;
    private System.Random RandomGenerator;
    private int counterTiles;
    private bool isChoosing;
    private float heightChoice;
    private float edge;
    private int maxTiles;
    private int maxTypesOfTiles;

    // Start is called before the first frame update
    void Start()
    {
        counterTiles = 0;
        heightChoice = 0.2f;
        isChoosing = false;
        edge = 2.0f;
        maxTiles = 72;
        maxTypesOfTiles = 24;
        MotionManager = GameObject.Find("MotionManager");
        PrefabManager = GameObject.Find("PrefabManager");
        _motionManager = MotionManager.GetComponent<MotionManager>();
        _prefabManager = PrefabManager.GetComponent<PrefabManager>();
        RandomGenerator = new System.Random();
        Tiles = new GameObject[maxTiles];
        tileNames = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X" };
        tiles = new string[maxTiles];

        for (int i = 0; i < maxTiles; i++)
        {
            tiles[i] = tileNames[RandomGenerator.Next(0, maxTypesOfTiles)];
        }
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
                RaycastHit Hit = CameraHit();
                if (Hit.transform != null)
                {
                    if (Tiles[counterTiles - 1].transform.CompareTag(Hit.transform.tag))
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
        RaycastHit Hit = CameraHit();
        if (Hit.transform != null)
        {
            if (Hit.transform.tag == "GameController") // Later here should be prefab-plate tag
            {
                isChoosing = true;
                if (isNew)
                {
                    counterTiles++;
                    Tiles[counterTiles - 1] = AddPrefabByName(tiles[counterTiles - 1], Hit.point.x, heightChoice, Hit.point.z); // TODO: from point to squares
                    Tiles[counterTiles - 1].name += counterTiles;
                    Tiles[counterTiles - 1].AddComponent<BoxCollider>().size = new Vector3(edge, 1, edge);
                }
                else
                {
                    Tiles[counterTiles - 1].transform.position = new Vector3(Hit.point.x, heightChoice, Hit.point.z);
                }
            }
        }
    }

    // This method adds a given prefab and returns an GameObject
    public GameObject AddPrefabByName(string prefabName, float x, float y, float z)
    {
        return Instantiate(_prefabManager.GetPrefabByName(prefabName), new Vector3(x, y, z), Quaternion.identity);
    }

    // This method return Hit where now cursor is
    private RaycastHit CameraHit()
    {
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        Physics.Raycast(Ray, out Hit);
        return Hit;
    }
}
