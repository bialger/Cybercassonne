using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;
    public GameObject F;
    public GameObject G;
    public GameObject H;
    public GameObject I;
    public GameObject J;
    public GameObject K;
    public GameObject L;
    public GameObject M;
    public GameObject N;
    public GameObject O;
    public GameObject P;
    public GameObject Q;
    public GameObject R;
    public GameObject S;
    public GameObject T;
    public GameObject U;
    public GameObject V;
    public GameObject W;
    public GameObject X;
    public GameObject ChoosingSquare;
    private Dictionary<string, GameObject> PrefabsFromNames;

    // Start is called before the first frame update
    void Start()
    {
        PrefabsFromNames = new Dictionary<string, GameObject>()
        {
            ["A"] = A,
            ["B"] = B,
            ["C"] = C,
            ["D"] = D,
            ["E"] = E,
            ["F"] = F,
            ["G"] = G,
            ["H"] = H,
            ["I"] = I,
            ["J"] = J,
            ["K"] = K,
            ["L"] = L,
            ["M"] = M,
            ["N"] = N,
            ["O"] = O,
            ["P"] = P,
            ["Q"] = Q,
            ["R"] = R,
            ["S"] = S,
            ["T"] = T,
            ["U"] = U,
            ["V"] = V,
            ["W"] = W,
            ["X"] = X,
            ["ChoosingSquare"] = ChoosingSquare
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This method returns an GameObject of given prefab by name
    public GameObject GetPrefabByName(string prefabName)
    {
        return PrefabsFromNames[prefabName];
    }
}
