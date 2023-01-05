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
        const int none = -1;
        const int road = 0;
        const int city = 1;
        const int field = 2;
        tileSides = new Dictionary<string, int[]>()
        {
            ["A"] = new int[] { field, field, field, road }, // up, right, down, left; up - z axis, left - x axis
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
            ["None"] = new int[] { none, none, none, none }
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This method returns an array of compatibility of two tiles (up, right, down, left)
    public bool[] AreCompatible(string newTileName, string oldTileName, int rotationType)
    {
        bool[] res = new bool[4];
        int[] oldTileSides = tileSides[oldTileName];
        if (rotationType == 1 || rotationType == -3)
        {
            res[0] = tileSides[newTileName].Contains(oldTileSides[3]);
            res[1] = tileSides[newTileName].Contains(oldTileSides[0]);
            res[2] = tileSides[newTileName].Contains(oldTileSides[1]);
            res[3] = tileSides[newTileName].Contains(oldTileSides[2]);
        }
        else if (rotationType == 2 || rotationType == -2)
        {
            res[0] = tileSides[newTileName].Contains(oldTileSides[2]);
            res[1] = tileSides[newTileName].Contains(oldTileSides[3]);
            res[2] = tileSides[newTileName].Contains(oldTileSides[0]);
            res[3] = tileSides[newTileName].Contains(oldTileSides[1]);
        }
        else if (rotationType == 3 || rotationType == -1)
        {
            res[0] = tileSides[newTileName].Contains(oldTileSides[1]);
            res[1] = tileSides[newTileName].Contains(oldTileSides[2]);
            res[2] = tileSides[newTileName].Contains(oldTileSides[3]);
            res[3] = tileSides[newTileName].Contains(oldTileSides[0]);
        }
        else
        {
            res[0] = tileSides[newTileName].Contains(oldTileSides[0]);
            res[1] = tileSides[newTileName].Contains(oldTileSides[1]);
            res[2] = tileSides[newTileName].Contains(oldTileSides[2]);
            res[3] = tileSides[newTileName].Contains(oldTileSides[3]);
        }
        return res;
    }

    // This method returns avalible tile rotation options
    public bool[] AvalibleRotations(string newTileName, string upperTileName, string rightTileName, string bottomTileName, string leftTileName)
    {
        bool[] res = new bool[4];
        int[] newTileSides = tileSides[newTileName];
        int upperTileSide = tileSides[upperTileName][2];
        int rightTileSide = tileSides[rightTileName][3];
        int bottomTileSide = tileSides[bottomTileName][0];
        int leftTileSide = tileSides[leftTileName][1];
        res[0] = (upperTileSide == newTileSides[0] || upperTileSide == -1) && (rightTileSide == newTileSides[1] || rightTileSide == -1) && (bottomTileSide == newTileSides[2] || bottomTileSide == -1) && (leftTileSide == newTileSides[3] || leftTileSide == -1);
        res[1] = (upperTileSide == newTileSides[3] || upperTileSide == -1) && (rightTileSide == newTileSides[0] || rightTileSide == -1) && (bottomTileSide == newTileSides[1] || bottomTileSide == -1) && (leftTileSide == newTileSides[2] || leftTileSide == -1);
        res[2] = (upperTileSide == newTileSides[2] || upperTileSide == -1) && (rightTileSide == newTileSides[3] || rightTileSide == -1) && (bottomTileSide == newTileSides[0] || bottomTileSide == -1) && (leftTileSide == newTileSides[1] || leftTileSide == -1);
        res[3] = (upperTileSide == newTileSides[1] || upperTileSide == -1) && (rightTileSide == newTileSides[2] || rightTileSide == -1) && (bottomTileSide == newTileSides[3] || bottomTileSide == -1) && (leftTileSide == newTileSides[0] || leftTileSide == -1);
        return res;
    }
}
