using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private Dictionary<string, int[]> tileSides;

    // Start is called before the first frame update
    void Start()
    {
        const int road = 0;
        const int city = 1;
        const int field = 2;
        tileSides = new Dictionary<string, int[]>()
        {
            ["A"] = new int[] { field, field, field, road }, // up, left, down, right; up - z axis, left - x axis
            ["B"] = new int[] { field, field, field, field },
            ["C"] = new int[] { city, city, city, city },
            ["D"] = new int[] { road, field, road, city },
            ["E"] = new int[] { field, field, field, city },
            ["F"] = new int[] { field, city, field, city },
            ["G"] = new int[] { field, city, field, city },
            ["H"] = new int[] { field, city, field, city },
            ["I"] = new int[] { city, field, field, city },
            ["J"] = new int[] { road, field, city, road },
            ["K"] = new int[] { road, road, city, field },
            ["L"] = new int[] { road, road, city, road },
            ["M"] = new int[] { field, field, city, city },
            ["N"] = new int[] { field, field, city, city },
            ["O"] = new int[] { road, road, city, city },
            ["P"] = new int[] { road, road, city, city },
            ["Q"] = new int[] { field, city, city, city },
            ["R"] = new int[] { field, city, city, city },
            ["S"] = new int[] { road, city, city, city },
            ["T"] = new int[] { road, city, city, city },
            ["U"] = new int[] { road, field, road, field },
            ["V"] = new int[] { road, road, field, field },
            ["W"] = new int[] { road, road, road, field },
            ["X"] = new int[] { road, road, road, road },

        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This method returns an array of compatibility of two tiles (up, right, down, left)
    public bool[] areCompatible(string newTileName, string oldTileName, int rotationType)
    {
        bool[] res = new bool[4];
        int[] oldTileSides = tileSides[oldTileName];
        if (rotationType == 1 || rotationType == -3)
        {
            int tmp = oldTileSides[3];
            oldTileSides[3] = oldTileSides[2];
            oldTileSides[2] = oldTileSides[1];
            oldTileSides[1] = oldTileSides[0];
            oldTileSides[0] = tmp;
        }
        if (rotationType == 2 || rotationType == -2)
        {
            int tmp = oldTileSides[2];
            oldTileSides[2] = oldTileSides[0];
            oldTileSides[0] = tmp;
            tmp = oldTileSides[3];
            oldTileSides[3] = oldTileSides[1];
            oldTileSides[1] = tmp;
        }
        if (rotationType == 3 || rotationType == -1)
        {
            int tmp = oldTileSides[0];
            oldTileSides[0] = oldTileSides[1];
            oldTileSides[1] = oldTileSides[2];
            oldTileSides[2] = oldTileSides[3];
            oldTileSides[3] = tmp;
        }
        res[0] = tileSides[newTileName].Contains(oldTileSides[0]);
        res[1] = tileSides[newTileName].Contains(oldTileSides[1]);
        res[2] = tileSides[newTileName].Contains(oldTileSides[2]);
        res[3] = tileSides[newTileName].Contains(oldTileSides[3]);
        return res;
    }
}
