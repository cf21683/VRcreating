using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Transformable))]
public class TransformableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Transformable script = (Transformable)target;
        
        
        if (GUILayout.Button("Activate Transformable"))
        {
            script.ActivateTransformable();
        }
        
        if (GUILayout.Button("Remove Transformable Script"))
        {
            
            if (EditorUtility.DisplayDialog("Remove Transformable script",
                    "Are you sure you want to remove the Transformable script?",
                    "Yes", "No"))
            {
                Undo.RecordObject(script, "Remove Transformable script");
                script.DeleteTransformable(); 
            }
        }
    }
}