using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureGenerator : MonoBehaviour
{
    public GameObject sofaAndTableAndTvPrefab; 
	
	public Vector3 roomSize = new Vector3(10, 3, 10);
	float wallThickness = 0.4f;
    float wallHeight = 3; 
    void Start()
    {
        GenerateFurniture();
    }

    void GenerateFurniture()
    {
        Vector3 sofaAndTableAndTvPosition = new Vector3(-3.6f, 0.8f, 4.45f);
        Quaternion sofaAndTableAndTvRotation = Quaternion.Euler(0, -180, 0);
        GameObject sofaAndTableAndTv =
            Instantiate(sofaAndTableAndTvPrefab, sofaAndTableAndTvPosition, sofaAndTableAndTvRotation); 
       sofaAndTableAndTv.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
    }
}
