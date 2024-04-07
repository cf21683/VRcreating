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

        string rotationKey = obj.name + "_rotation";
        Quaternion rotation = obj.transform.rotation;
        string rotationValue = rotation.x + "," + rotation.y + "," + rotation.z + "," + rotation.w;
        EditorPrefs.SetString(rotationKey, rotationValue);
    }
    Debug.Log("PlayMode Changes Saved");
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

        // check if the position key exists
        if(EditorPrefs.HasKey(positionKey)){
            string positionValue = EditorPrefs.GetString(positionKey);
            string[] values = positionValue.Split(',');
            Vector3 position = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            obj.transform.position = position;
            Debug.Log($"Applying saved position for object: {obj.name} to {position}");
        }

        string rotationKey = obj.name + "_rotation";
        if(EditorPrefs.HasKey(rotationKey)){
            string rotationValue = EditorPrefs.GetString(rotationKey);
            string[] values = rotationValue.Split(',');
            Quaternion rotation = new Quaternion(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]));
            obj.transform.rotation = rotation;
            Debug.Log($"Applying saved rotation for object: {obj.name} to {rotation}");
        }


        EditorPrefs.DeleteKey(positionKey);
        EditorPrefs.DeleteKey(rotationKey);
    }
    Debug.Log("PlayMode Changes Applied");
   }
}
