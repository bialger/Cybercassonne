using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject AddPrefab(GameObject prefab, float x, float y, float z)
    {
        return Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
    }

    void RotateObject(GameObject Obj, float angleX, float angleY, float angleZ)
    {
        Obj.transform.Rotate(angleX, angleY, angleZ);
    }

    void MoveObject(GameObject Obj, float velX, float velY, float velZ, float deltaTime)
    {
        Obj.transform.position += new Vector3(velX, velY, velZ) * deltaTime;
    }
}
