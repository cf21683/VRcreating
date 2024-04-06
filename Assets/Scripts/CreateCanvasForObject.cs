using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreateCanvasForObject : MonoBehaviour
{

   public void CreateCanvas()
    {
// load the canvas prefab and set the object to the canvas
    GameObject canvasPrefab = Resources.Load<GameObject>("ColorCanvas");
    if (canvasPrefab != null) {
        GameObject canvasInstance = Instantiate(canvasPrefab, transform);
        Renderer currentRenderer = GetComponent<Renderer>();
        canvasPrefab.transform.localScale = new Vector3(1f, 1f, 1f);
        if(currentRenderer != null)
            {
                
                ColorChange colorChangeScript = canvasInstance.GetComponent<ColorChange>();
                if(colorChangeScript != null)
                {
                    colorChangeScript.SetRenderer(currentRenderer);
                }
                
            }
        } 
    }

   

    void Start(){
    }

    void Awake()
    {
        CreateCanvas();
    }
}
