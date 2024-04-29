using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomGenerator : MonoBehaviour
{
    private List<GameObject> generatedObjects = new List<GameObject>();
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject doorPrefab;
	public GameObject windowPrefab;
	public Transform player;


    public float windowHeight = 2.0f;
    public float windowWidth = 1.0f;

    public Material[] wallMaterials;
    
    public Vector3 roomSize = new Vector3(10, 3, 10);

    void Start()
    {
        
    }

    public void GenerateRoom()
    {
        DestroyGeneratedObjects();
        GameObject floor = Instantiate(floorPrefab, transform.position, Quaternion.identity);
        floor.transform.localScale = new Vector3(roomSize.x, 1, roomSize.z);

        generatedObjects.Add(floor);

        float desiredFloorThickness = 1.2f;

        //get the collider
        BoxCollider floorCollider = floor.GetComponent<BoxCollider>();
        if (floorCollider == null)
        {
            floorCollider = floor.AddComponent<BoxCollider>();
        }

        // set the height of collider
        Vector3 colliderSize = floorCollider.size;
        colliderSize.y = desiredFloorThickness; 
        floorCollider.size = colliderSize;

        float wallThickness = 0.4f;
        float wallHeight = 3;
        
        
        
        float doorPositionX = Random.Range(-roomSize.x / 2 + 1.5f, roomSize.x / 2 - 1.5f);
        Vector3 doorPosition = new Vector3(doorPositionX, 1.26f, -roomSize.z / 2 + wallThickness / 2);
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

       
      
            GameObject frontLeftWall = Instantiate(wallPrefab, new Vector3(-roomSize.x / 2 -0.2f+ frontLeftWallWidth / 2, wallHeight / 2, -roomSize.z / 2), Quaternion.identity);
            CustomizeWall(frontLeftWall, index);
            frontLeftWall.transform.localScale = new Vector3(frontLeftWallWidth, wallHeight, wallThickness);
            generatedObjects.Add(frontLeftWall);

        
        
            GameObject frontRightWall = Instantiate(wallPrefab, new Vector3(doorPositionX + doorWidth / 2 +0.1f + frontRightWallWidth / 2, wallHeight / 2, -roomSize.z / 2), Quaternion.identity);
            CustomizeWall(frontRightWall, index);
            frontRightWall.transform.localScale = new Vector3(frontRightWallWidth, wallHeight, wallThickness);
            generatedObjects.Add(frontRightWall);
        
    }

    
    GameObject GenerateOtherWalls(float wallHeight, float wallThickness, int index)
    {
        GameObject[] walls = new GameObject[3];

        walls[0] = Instantiate(wallPrefab, new Vector3(0, wallHeight / 2, roomSize.z / 2), Quaternion.identity);
        CustomizeWall(walls[0], index);
        walls[0].transform.localScale = new Vector3(roomSize.x, wallHeight, wallThickness);
        generatedObjects.Add(walls[0]);

        walls[1] = Instantiate(wallPrefab, new Vector3(-roomSize.x / 2, wallHeight / 2, 0), Quaternion.Euler(0, 90, 0));
        CustomizeWall(walls[1], index);
        walls[1].transform.localScale = new Vector3(roomSize.z, wallHeight, wallThickness);
        generatedObjects.Add(walls[1]);

        walls[2] = Instantiate(wallPrefab, new Vector3(roomSize.x / 2, wallHeight / 2, 0), Quaternion.Euler(0, 90, 0));
        CustomizeWall(walls[2], index);
        walls[2].transform.localScale = new Vector3(roomSize.z, wallHeight, wallThickness);
        generatedObjects.Add(walls[2]);

       
        int randomIndex = Random.Range(0, walls.Length);
        return walls[randomIndex];
    }

    // generate a door according to the position
    float GenerateDoor(Vector3 position)
    {
        Quaternion doorRotation = Quaternion.identity; 
        GameObject door = Instantiate(doorPrefab, position, doorRotation);
		door.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        generatedObjects.Add(door);

        float doorWidth = door.transform.localScale.x * 3f;
		BoxCollider doorCollider = door.AddComponent<BoxCollider>();
		Rigidbody rb = door.AddComponent<Rigidbody>();
		
		rb.isKinematic = true;
        return doorWidth;
    }
    

    //apply different material to the wall
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
    
    // generate a window on the wall
	void GenerateWindow(GameObject wall){
				

		Vector3 windowPosition = wall.transform.position;
		Quaternion windowRotation;

    
		if (Mathf.Approximately(wall.transform.position.z, 0))
    {	
			float randomZ = Random.Range(-roomSize.z / 2 + 1f, roomSize.z / 2 - 1f);
        
        if (wall.transform.position.x > 0)// if the window is on the left side of the wall
        {
           	windowPosition += new Vector3(-0.2f,0,randomZ);
            windowRotation = Quaternion.Euler(-90, -90, 0); 
        }
        else    // if the window is on the right side of the wall
        {
            windowPosition += new Vector3(+0.2f,0,randomZ);
            windowRotation = Quaternion.Euler(-90, 90, 0); 
        }
    }
    else
    {
        
        if (wall.transform.position.z > 0) // if the window is on the back wall
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
        generatedObjects.Add(windowInstance);
	}


    public void DestroyGeneratedObjects(){
        // delete all the objects that were generated
        Debug.Log("DestroyGeneratedObjects called");
         foreach (GameObject obj in generatedObjects)
        {
            if (Application.isPlaying)
            {
                Destroy(obj);
            }
            else
            {
                DestroyImmediate(obj);
            }
        }
        generatedObjects.Clear();
    }
}