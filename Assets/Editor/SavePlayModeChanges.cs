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
    // find the each object and save the position
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
    }
    
   }

   private static void OnPlayModeStateChanged(PlayModeStateChange state){
        switch (state)
        {
            case PlayModeStateChange.ExitingPlayMode:
                SaveChanges(); // Save changes before exiting Play Mode
                break;
            case PlayModeStateChange.EnteredEditMode:
                ApplySavedPositions(); // Apply saved positions after exiting Play Mode
                break;
        }
    }

   private static void ApplySavedPositions(){
    Debug.Log("Starting to apply saved positions.");
    foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Object")){
        string positionKey = obj.name + "_position";

        // check if the position key exists and apply position
        if(EditorPrefs.HasKey(positionKey)){
            string positionValue = EditorPrefs.GetString(positionKey);
            string[] positionValues = positionValue.Split(',');
            Vector3 position = new Vector3(float.Parse(positionValues[0]), float.Parse(positionValues[1]), float.Parse(positionValues[2]));
            obj.transform.position = position;
            Debug.Log($"Applying saved position for object: {obj.name} to {position}");
        }

        // check if the rotation key exists and apply rotation
        string rotationKey = obj.name + "_rotation";
        if(EditorPrefs.HasKey(rotationKey)){
            string rotationValue = EditorPrefs.GetString(rotationKey);
            string[] rotationValues = rotationValue.Split(',');
            Quaternion rotation = new Quaternion(float.Parse(rotationValues[0]), float.Parse(rotationValues[1]), float.Parse(rotationValues[2]), float.Parse(rotationValues[3]));
            obj.transform.rotation = rotation;
            Debug.Log($"Applying saved rotation for object: {obj.name} to {rotation}");
        }

        string scaleKey = obj.name + "_scale";
        if(EditorPrefs.HasKey(scaleKey)){
            string scaleValue = EditorPrefs.GetString(scaleKey);
            string[] scaleValues = scaleValue.Split(',');
            Vector3 scale = new Vector3(float.Parse(scaleValues[0]), float.Parse(scaleValues[1]), float.Parse(scaleValues[2]));
            obj.transform.localScale = scale;
            Debug.Log($"Applying saved scale for object: {obj.name} to {scale}");
        }
        EditorPrefs.DeleteKey(positionKey);
        EditorPrefs.DeleteKey(rotationKey);
        EditorPrefs.DeleteKey(scaleKey);
    }
    Debug.Log("PlayMode Changes Applied");
   }
}
