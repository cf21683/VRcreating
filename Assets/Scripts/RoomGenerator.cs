using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject doorPrefab;

    public Material[] wallMaterials;
    
    public Vector3 roomSize = new Vector3(10, 3, 10);

    void Start()
    {
        GenerateRoom();
        GenerateDoor();
    }

    void GenerateRoom()
    {
        
        GameObject floor = Instantiate(floorPrefab, transform.position, Quaternion.identity);
        floor.transform.localScale = new Vector3(roomSize.x, 1, roomSize.z);

        float wallThickness = 0.4f;
        float wallHeight = 3;

        int index = Random.Range(0, wallMaterials.Length);
      
        GameObject frontWall = Instantiate(wallPrefab, new Vector3(0, wallHeight / 2, -roomSize.z / 2), Quaternion.identity);
        CustomizeWall(frontWall,index);
        frontWall.transform.localScale = new Vector3(roomSize.x, wallHeight, wallThickness); 

        GameObject backWall = Instantiate(wallPrefab, new Vector3(0, wallHeight / 2, roomSize.z / 2), Quaternion.identity);
        CustomizeWall(backWall,index);
        backWall.transform.localScale = new Vector3(roomSize.x, wallHeight, wallThickness); 
        
      
        GameObject leftWall = Instantiate(wallPrefab, new Vector3(-roomSize.x / 2, wallHeight / 2, 0), Quaternion.Euler(0, 90, 0));
        CustomizeWall(leftWall,index);
        leftWall.transform.localScale = new Vector3(roomSize.z, wallHeight, wallThickness);

        GameObject rightWall = Instantiate(wallPrefab, new Vector3(roomSize.x / 2, wallHeight / 2, 0), Quaternion.Euler(0, 90, 0));
        CustomizeWall(rightWall,index);
        rightWall.transform.localScale = new Vector3(roomSize.z, wallHeight, wallThickness); 
    }

    void GenerateDoor()
    {
        float wallHeight = 3; 
        float doorWidth = 1; 
        
        
        float minX = -roomSize.x / 2 + doorWidth / 2; 
        float maxX = roomSize.x / 2 - doorWidth / 2; 
        float randomX = Random.Range(minX, maxX); 

        
        Vector3 doorPosition = new Vector3(randomX, wallHeight / 2, -roomSize.z / 2 + 0.2f); 
        Quaternion doorRotation = Quaternion.Euler(0, 0, 0); 
        GameObject door = Instantiate(doorPrefab, doorPosition, doorRotation);
        door.transform.localScale = new Vector3(0.2f,0.2f,0.18f);
    }

    void CustomizeWall(GameObject wall,int index)
    {
        if (wallMaterials.Length > 0)
        {
            Renderer wallRenderer = wall.GetComponent<Renderer>();
            if (wallRenderer != null)
            {
                wallRenderer.material = wallMaterials[index];
            }
        }
    }
}