using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCanvasForObject : MonoBehaviour
{

   public void CreateCanvas()
    {
    GameObject canvasPrefab = Resources.Load<GameObject>("ColorCanvas");
    if (canvasPrefab != null) {
        GameObject canvasInstance = Instantiate(canvasPrefab);
        Renderer currentRenderer = GetComponent<Renderer>();

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
        CreateCanvas();
    }
}
