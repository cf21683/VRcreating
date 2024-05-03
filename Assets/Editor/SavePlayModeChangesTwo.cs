using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Serialization creates a special class

[System.Serializable]
public class ObjectState
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Color color;
}

[InitializeOnLoad]
public class SavePlayModeChangesTwo
{
    // Dictionary is a collection of key-value pairs
    private static Dictionary<GameObject, ObjectState> objectStates = new Dictionary<GameObject, ObjectState>();

    static SavePlayModeChangesTwo()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void SaveChanges()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object2");
        foreach (GameObject obj in objects)
        {
            // if the object is not in the dictionary, add it and create a custom class
            if (!objectStates.ContainsKey(obj))
            {
                objectStates[obj] = new ObjectState();
            }

            Undo.RecordObject(obj, "Save Play Mode Changes");
            objectStates[obj].position = obj.transform.position;
            objectStates[obj].rotation = obj.transform.rotation;
            objectStates[obj].scale = obj.transform.localScale;

            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                Undo.RecordObject(renderer, "Save Play Mode Changes");
                objectStates[obj].color = renderer.material.color;
            }
        }
    }

    private static void ApplySaved()
    {
        foreach (var information in objectStates)
        {
            GameObject obj = information.Key;
            ObjectState state = information.Value;

            Undo.RecordObject(obj.transform, "Apply Play Mode Changes");
            obj.transform.position = state.position;
            obj.transform.rotation = state.rotation;
            obj.transform.localScale = state.scale;

            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                Undo.RecordObject(renderer, "Apply Play Mode Changes");
                // use sharedMaterial instead of material to avoid leak materials into the scene
                renderer.sharedMaterial.color = state.color;
            }
        }
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            SaveChanges();
        }
        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            ApplySaved();
        }
    }
}