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
        
        GameObject floor = Instantiate(floorPrefab, transform.position, Quaternion.identity);
        floor.transform.localScale = new Vector3(roomSize.x, 1, roomSize.z);

        float wallThickness = 0.4f;
        float wallHeight = 3; 
        
      
        GameObject frontWall = Instantiate(wallPrefab, new Vector3(0, wallHeight / 2, -roomSize.z / 2), Quaternion.identity);
        frontWall.transform.localScale = new Vector3(roomSize.x, wallHeight, wallThickness); 

        GameObject backWall = Instantiate(wallPrefab, new Vector3(0, wallHeight / 2, roomSize.z / 2), Quaternion.identity);
        backWall.transform.localScale = new Vector3(roomSize.x, wallHeight, wallThickness); 
        
      
        GameObject leftWall = Instantiate(wallPrefab, new Vector3(-roomSize.x / 2, wallHeight / 2, 0), Quaternion.Euler(0, 90, 0));
        leftWall.transform.localScale = new Vector3(roomSize.z, wallHeight, wallThickness); 

        GameObject rightWall = Instantiate(wallPrefab, new Vector3(roomSize.x / 2, wallHeight / 2, 0), Quaternion.Euler(0, 90, 0));
        rightWall.transform.localScale = new Vector3(roomSize.z, wallHeight, wallThickness); 
    }
}