using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFacePlayer : MonoBehaviour
{

    public GameObject canvas;
    
    public Transform player;
    
    public float distanceFromPlayer = 0.3f;
    public float heightOffset = 1.3f; 

    void Start()
    {
    
    }

    
    void Update()
    {

        if (!player) // if not found player then find the player in the obejct list
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject)
        {
            player = playerGameObject.transform;
        }
    }

        if (canvas.activeSelf && player != null) // if the canvas is active and the player is not null then place the canvas to the player
        {
            
            Vector3 canvasPosition = player.position + player.forward * distanceFromPlayer;
        
        
            canvasPosition.y += heightOffset;
        
            
            canvas.transform.position = canvasPosition;
        
            
            Vector3 lookDirection = player.position - canvas.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            canvas.transform.rotation = lookRotation;

            Vector3 rotation = canvas.transform.eulerAngles;
            rotation.x = 0; 
            canvas.transform.eulerAngles = rotation;
            canvas.transform.Rotate(0, 180f, 0);
        }
    }

}
