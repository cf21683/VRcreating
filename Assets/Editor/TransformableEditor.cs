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
    }
}