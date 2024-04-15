#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[InitializeOnLoad]
public class SavePlayModeChanges
{
    static SavePlayModeChanges()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

   private static void SaveChanges(){
    Debug.Log("Starting to save play mode changes.");
    // find the each object 
    foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Object")){
        // save the position
        string positionKey = obj.name + "_position";
        Vector3 position = obj.transform.position;
        string positionValue = position.x + "," + position.y + "," + position.z;
        Debug.Log($"Saving position for object: {obj.name} at {position}");
        EditorPrefs.SetString(positionKey, positionValue);

        // save the rotation
        string rotationKey = obj.name + "_rotation";
        Quaternion rotation = obj.transform.rotation;
        string rotationValue = rotation.x + "," + rotation.y + "," + rotation.z + "," + rotation.w;
        EditorPrefs.SetString(rotationKey, rotationValue);

        // save the scale
        string scaleKey = obj.name + "_scale";
        Vector3 scale = obj.transform.localScale;
        string scaleValue = scale.x.ToString("F3") + "," + scale.y.ToString("F3") + "," + scale.z.ToString("F3");
        EditorPrefs.SetString(scaleKey, scaleValue);

        // save the color
        Renderer renderer = obj.GetComponent<Renderer>();
        if(renderer != null){
            Color color = renderer.material.color;
            string colorKey = obj.name + "_color";
            string colorValue = color.r + "," + color.g + "," + color.b + "," + color.a;
            EditorPrefs.SetString(colorKey, colorValue);
        }
    }  
    
   }

   private static void OnPlayModeStateChanged(PlayModeStateChange state){
        switch (state)
        {
            case PlayModeStateChange.ExitingPlayMode:
                SaveChanges(); // Save changes before exiting Play Mode
                break;
            case PlayModeStateChange.EnteredEditMode:
                ApplySaved(); // Apply saved positions after exiting Play Mode
                break;
        }
    }

   private static void ApplySaved(){
    Debug.Log("Starting to apply saved changes.");
    foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Object")){
        string positionKey = obj.name + "_position";
        string rotationKey = obj.name + "_rotation";
        string scaleKey = obj.name + "_scale";
        string colorKey = obj.name + "_color";

        // check if the position key exists and apply position
        if(EditorPrefs.HasKey(positionKey)){
            string positionValue = EditorPrefs.GetString(positionKey);
            string[] positionValues = positionValue.Split(',');
            Vector3 position = new Vector3(float.Parse(positionValues[0]), float.Parse(positionValues[1]), float.Parse(positionValues[2]));
            obj.transform.position = position;
            Debug.Log($"Applying saved position for object: {obj.name} to {position}");
        }

        // check if the rotation key exists and apply rotation
     
        if(EditorPrefs.HasKey(rotationKey)){
            string rotationValue = EditorPrefs.GetString(rotationKey);
            string[] rotationValues = rotationValue.Split(',');
            Quaternion rotation = new Quaternion(float.Parse(rotationValues[0]), float.Parse(rotationValues[1]), float.Parse(rotationValues[2]), float.Parse(rotationValues[3]));
            obj.transform.rotation = rotation;
            Debug.Log($"Applying saved rotation for object: {obj.name} to {rotation}");
        }


        // check if the scale key exists and apply scale
        if(EditorPrefs.HasKey(scaleKey)){
            string scaleValue = EditorPrefs.GetString(scaleKey);
            string[] scaleValues = scaleValue.Split(',');
            Vector3 scale = new Vector3(float.Parse(scaleValues[0]), float.Parse(scaleValues[1]), float.Parse(scaleValues[2]));
            obj.transform.localScale = scale;
            Debug.Log($"Applying saved scale for object: {obj.name} to {scale}");
        }

        // check if the color key exists and apply color        
        if (EditorPrefs.HasKey(colorKey))
        {
            string colorValue = EditorPrefs.GetString(colorKey);
            string[] colorValues = colorValue.Split(',');
            Color color = new Color(
                float.Parse(colorValues[0]),
                float.Parse(colorValues[1]),
                float.Parse(colorValues[2]),
                float.Parse(colorValues[3]));
                
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial.color = color;
                Debug.Log($"Applying saved color for object: {obj.name} to {color}");
            }
        }

        EditorPrefs.DeleteKey(positionKey);
        EditorPrefs.DeleteKey(rotationKey);
        EditorPrefs.DeleteKey(scaleKey);
        EditorPrefs.DeleteKey(colorKey);
    }
    Debug.Log("PlayMode Changes Applied");
   }
}
#endif