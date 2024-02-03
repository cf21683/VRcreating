using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public Vector3 roomSize = new Vector3(10, 3, 10);

    void Start()
    {
        GenerateRoom();
    }

    void GenerateRoom()
    {
        GameObject woodfloor = Instantiate(floorPrefab, transform.position,Quaternion.identity);
        woodfloor.transform.localScale = new Vector3(10, 1, 10);

        for (int i = 0; i < roomSize.x; i++)
        {
            Instantiate(wallPrefab, new Vector3(i, 0, 0), Quaternion.identity); 
            Instantiate(wallPrefab, new Vector3(i, 0, roomSize.z), Quaternion.identity); 
        }

        for (int i = 0; i < roomSize.z; i++)
        {
            Instantiate(wallPrefab, new Vector3(0, 0, i), Quaternion.identity); 
            Instantiate(wallPrefab, new Vector3(roomSize.x, 0, i), Quaternion.identity); 
        }
    }
}