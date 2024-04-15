using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomGenerator))]
public class RoomGeneratorEditor : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        RoomGenerator roomGenerator = (RoomGenerator)target;

        // a button use to generate the room
        if(GUILayout.Button("Generate Room")){
            roomGenerator.GenerateRoom();
        }

        if(GUILayout.Button("Clear Room")){
            roomGenerator.DestroyGeneratedObjects();
        }
    }
}
