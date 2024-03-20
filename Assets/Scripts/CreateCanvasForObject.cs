using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCanvasForObject : MonoBehaviour
{

   public void CreateCanvas()
    {
    GameObject canvasPrefab = Resources.Load<GameObject>("ColorCanvas");
    if (canvasPrefab != null) {
        Debug.Log("Creating Canvas Prefab");
        Instantiate(canvasPrefab);
    } else {
        Debug.LogError("Failed to load Canvas Prefab from Resources!");
    }
}
    void Start(){
        CreateCanvas();
    }
}
