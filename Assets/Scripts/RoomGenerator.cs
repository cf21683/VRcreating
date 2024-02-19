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
    }

    void GenerateRoom()
    {
        GameObject floor = Instantiate(floorPrefab, transform.position, Quaternion.identity);
        floor.transform.localScale = new Vector3(roomSize.x, 1, roomSize.z);

        float wallThickness = 0.4f;
        float wallHeight = 3;
        
        
        
        float doorPositionX = Random.Range(-roomSize.x / 2 + 1.5f, roomSize.x / 2 - 1.5f);
        Vector3 doorPosition = new Vector3(doorPositionX, wallHeight / 2 + 0.25f, -roomSize.z / 2 + wallThickness / 2);
        float doorWidth = GenerateDoor(doorPosition);
        
        
        int index = Random.Range(0, wallMaterials.Length);

        
        GenerateFrontWallSegments(doorPositionX, doorWidth, wallHeight, wallThickness, index);
        GenerateOtherWalls(wallHeight, wallThickness, index);

      
        
        
    }

    void GenerateFrontWallSegments(float doorPositionX, float doorWidth, float wallHeight, float wallThickness, int index)
    {
        float frontLeftWallWidth = doorPositionX + roomSize.x / 2 - doorWidth * 0.25f / 2;
        float frontRightWallWidth = roomSize.x / 2 - doorPositionX - doorWidth * 0.25f / 2;

       
      
            GameObject frontLeftWall = Instantiate(wallPrefab, new Vector3(-roomSize.x / 2 + frontLeftWallWidth / 2, wallHeight / 2, -roomSize.z / 2), Quaternion.identity);
            CustomizeWall(frontLeftWall, index);
            frontLeftWall.transform.localScale = new Vector3(frontLeftWallWidth, wallHeight, wallThickness);
        

        
        
            GameObject frontRightWall = Instantiate(wallPrefab, new Vector3(doorPositionX + doorWidth / 2 + frontRightWallWidth / 2, wallHeight / 2, -roomSize.z / 2), Quaternion.identity);
            CustomizeWall(frontRightWall, index);
            frontRightWall.transform.localScale = new Vector3(frontRightWallWidth, wallHeight, wallThickness);
        
    }

    void GenerateOtherWalls(float wallHeight, float wallThickness, int index)
    {
        
        GameObject backWall = Instantiate(wallPrefab, new Vector3(0, wallHeight / 2, roomSize.z / 2), Quaternion.identity);
        CustomizeWall(backWall, index);
        backWall.transform.localScale = new Vector3(roomSize.x, wallHeight, wallThickness); 
        
       
        GameObject leftWall = Instantiate(wallPrefab, new Vector3(-roomSize.x / 2, wallHeight / 2, 0), Quaternion.Euler(0, 90, 0));
        CustomizeWall(leftWall, index);
        leftWall.transform.localScale = new Vector3(roomSize.z, wallHeight, wallThickness);

        
        GameObject rightWall = Instantiate(wallPrefab, new Vector3(roomSize.x / 2, wallHeight / 2, 0), Quaternion.Euler(0, 90, 0));
        CustomizeWall(rightWall, index);
        rightWall.transform.localScale = new Vector3(roomSize.z, wallHeight, wallThickness); 
    }

    float GenerateDoor(Vector3 position)
    {
        Quaternion doorRotation = Quaternion.identity; 
        GameObject door = Instantiate(doorPrefab, position, doorRotation);
        door.transform.localScale = new Vector3(0.25f, 0.25f, 0.1f);

        float doorWidth = door.transform.localScale.x * 3f;
        return doorWidth;
    }

    void CustomizeWall(GameObject wall, int index)
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
