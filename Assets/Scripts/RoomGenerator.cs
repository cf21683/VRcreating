using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject doorPrefab;
	public GameObject windowPrefab;

    public float windowHeight = 2.0f;
    public float windowWidth = 1.0f;

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

        GameObject windowWall = GenerateOtherWalls(wallHeight, wallThickness, index);
    	GenerateWindow(windowWall);
 
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

    
    GameObject GenerateOtherWalls(float wallHeight, float wallThickness, int index)
    {
        GameObject[] walls = new GameObject[3];

        walls[0] = Instantiate(wallPrefab, new Vector3(0, wallHeight / 2, roomSize.z / 2), Quaternion.identity);
        CustomizeWall(walls[0], index);
        walls[0].transform.localScale = new Vector3(roomSize.x, wallHeight, wallThickness);

        walls[1] = Instantiate(wallPrefab, new Vector3(-roomSize.x / 2, wallHeight / 2, 0), Quaternion.Euler(0, 90, 0));
        CustomizeWall(walls[1], index);
        walls[1].transform.localScale = new Vector3(roomSize.z, wallHeight, wallThickness);

        walls[2] = Instantiate(wallPrefab, new Vector3(roomSize.x / 2, wallHeight / 2, 0), Quaternion.Euler(0, 90, 0));
        CustomizeWall(walls[2], index);
        walls[2].transform.localScale = new Vector3(roomSize.z, wallHeight, wallThickness);

       
        int randomIndex = Random.Range(0, walls.Length);
        return walls[randomIndex];
    }

    float GenerateDoor(Vector3 position)
    {
        Quaternion doorRotation = Quaternion.identity; 
        GameObject door = Instantiate(doorPrefab, position, doorRotation);
		door.transform.localScale = new Vector3(0.25f, 0.25f, 0.1f);

	
		doorInteraction doorInteract = door.AddComponent<doorInteraction>();
		Image doorLoadingUI = GameObject.Find("Canvas/doorLoadingUI").GetComponent<Image>();
		doorInteract.doorLoading = doorLoadingUI;
   
        float doorWidth = door.transform.localScale.x * 3f;
		BoxCollider doorCollider = door.AddComponent<BoxCollider>();
		Rigidbody rb = door.AddComponent<Rigidbody>();
		
		rb.isKinematic = true;
        return doorWidth;
    }

    void CustomizeWall(GameObject wall, int index)
    {
        if (wallMaterials.Length > 0)
        {
			BoxCollider collider = wall.AddComponent<BoxCollider>();
			Rigidbody rb = wall.AddComponent<Rigidbody>();
			rb.isKinematic = true;
            Renderer wallRenderer = wall.GetComponent<Renderer>();
            if (wallRenderer != null)
            {
                wallRenderer.material = wallMaterials[index];
            }
        }
    }

	void GenerateWindow(GameObject wall){
				

		Vector3 windowPosition = wall.transform.position;
		Quaternion windowRotation;

		if (Mathf.Approximately(wall.transform.position.z, 0))
    {	
			float randomZ = Random.Range(-roomSize.z / 2 + 1f, roomSize.z / 2 - 1f);
        
        if (wall.transform.position.x > 0)
        {
           	windowPosition += new Vector3(-0.2f,0,randomZ);
            windowRotation = Quaternion.Euler(-90, -90, 0); 
        }
        else
        {
            windowPosition += new Vector3(+0.2f,0,randomZ);
            windowRotation = Quaternion.Euler(-90, 90, 0); 
        }
    }
    else
    {
        
        if (wall.transform.position.z > 0)
        {	
			float randomX = Random.Range(0.0f, roomSize.x / 2 - 1f);
           	windowPosition += new Vector3(randomX,0,-0.2f);
            windowRotation = Quaternion.Euler(-90, 180, 0); 
        }
        else
        {
           
            windowRotation = Quaternion.identity; 
        }
    }

        
 
        GameObject windowInstance = Instantiate(windowPrefab, windowPosition, windowRotation);
	}
}
