using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject MotionManager;
    private GameObject PrefabManager;
    private GameObject TileManager;
    private GameObject[] Tiles;
    private List<GameObject> ChoosingSquares;
    private string[] tileNames;
    private string[] tiles;
    private MotionManager _motionManager;
    private PrefabManager _prefabManager;
    private TileManager _tileManager;
    private System.Random RandomGenerator;
    private int counterTiles;
    private bool isChoosing;
    private float heightChoice;
    private int edge;
    private int maxTiles;
    private int maxTypesOfTiles;

    // Start is called before the first frame update
    void Start()
    {
        counterTiles = 0;
        heightChoice = 0.2f;
        isChoosing = false;
        edge = 2;
        maxTiles = 72;
        maxTypesOfTiles = 24;
        MotionManager = GameObject.Find("MotionManager");
        PrefabManager = GameObject.Find("PrefabManager");
        TileManager = GameObject.Find("TileManager");
        _motionManager = MotionManager.GetComponent<MotionManager>();
        _prefabManager = PrefabManager.GetComponent<PrefabManager>();
        _tileManager = TileManager.GetComponent<TileManager>();
        RandomGenerator = new System.Random();
        Tiles = new GameObject[maxTiles];
        ChoosingSquares = new List<GameObject>();
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
        if (ChoosingSquares.Count == 0 && counterTiles == 0)
        {
            ChoosingSquares.Add(AddPrefabByName("ChoosingSquare", 0, 0, 0));
        }
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

                        foreach (GameObject oldSquare in ChoosingSquares)
                        {
                            Destroy(oldSquare);
                        }

                        ChoosingSquares = new List<GameObject>();
                        if (counterTiles < maxTiles)
                        {
                            int fieldSide = (maxTiles + 1) * edge * 2;
                            int[,] putAllSquares = new int[fieldSide, fieldSide];

                            for (int i = 0; i < fieldSide; i++)
                            {
                                for (int j = 0; j < fieldSide; j++)
                                {
                                    putAllSquares[i, j] = 2;
                                }
                            }

                            for (int i = 0; i < counterTiles; i++)
                            {
                                int xOfTheTile = (int)Tiles[i].transform.position.x + fieldSide / 2;
                                int zOfTheTile = (int)Tiles[i].transform.position.z + fieldSide / 2;
                                bool[] putSquares = _tileManager.areCompatible(tiles[counterTiles], tiles[i], (int)(Tiles[i].transform.rotation.eulerAngles.y / 90));
                                putAllSquares[xOfTheTile, zOfTheTile] = 0;
                                if (putAllSquares[xOfTheTile, zOfTheTile + edge] == 2 || putAllSquares[xOfTheTile, zOfTheTile + edge] == 1)
                                    putAllSquares[xOfTheTile, zOfTheTile + edge] = Convert.ToInt32(putSquares[0]);
                                if (putAllSquares[xOfTheTile + edge, zOfTheTile] == 2 || putAllSquares[xOfTheTile + edge, zOfTheTile] == 1)
                                    putAllSquares[xOfTheTile + edge, zOfTheTile] = Convert.ToInt32(putSquares[1]);
                                if (putAllSquares[xOfTheTile, zOfTheTile - edge] == 2 || putAllSquares[xOfTheTile, zOfTheTile - edge] == 1)
                                    putAllSquares[xOfTheTile, zOfTheTile - edge] = Convert.ToInt32(putSquares[2]);
                                if (putAllSquares[xOfTheTile - edge, zOfTheTile] == 2 || putAllSquares[xOfTheTile - edge, zOfTheTile] == 1)
                                    putAllSquares[xOfTheTile - edge, zOfTheTile] = Convert.ToInt32(putSquares[3]);
                            }

                            for (int i = 0; i < fieldSide; i++)
                            {
                                for (int j = 0; j < fieldSide; j++)
                                {
                                    if (putAllSquares[i, j] == 1)
                                        ChoosingSquares.Add(AddPrefabByName("ChoosingSquare", i - fieldSide / 2, 0, j - fieldSide / 2));
                                }
                            }
                        }
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
            if (_prefabManager.GetPrefabByName("ChoosingSquare").transform.CompareTag(Hit.transform.tag) && counterTiles <= maxTiles)
            {
                isChoosing = true;
                if (isNew)
                {
                    counterTiles++;
                    Tiles[counterTiles - 1] = AddPrefabByName(tiles[counterTiles - 1], Hit.transform.position.x, heightChoice, Hit.transform.position.z);
                    Tiles[counterTiles - 1].AddComponent<BoxCollider>().size = new Vector3(edge, 0.5f, edge); // TODO: replace y shape with smth relevant
                    Tiles[counterTiles - 1].name += counterTiles;
                    Tiles[counterTiles - 1].transform.tag = "CurrentTile";
                    if (counterTiles > 1)
                    {
                        Tiles[counterTiles - 2].transform.tag = "OldTile";
                    }
                }
                else
                {
                    Tiles[counterTiles - 1].transform.position = new Vector3(Hit.transform.position.x, heightChoice, Hit.transform.position.z);
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