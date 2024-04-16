using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomGenerator))]
public class RoomGeneratorEditor : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        RoomGenerator roomGenerator = (RoomGenerator)target;

        // a button use to generate the room object
        if(GUILayout.Button("Generate Room")){
            roomGenerator.GenerateRoom();
        }

        // a button use to delete the room object
        if(GUILayout.Button("Clear Room")){

             if (EditorUtility.DisplayDialog("Remove room object",
                    "Are you sure you want to remove the room object?",
                    "Yes", "No"))
            {
                Undo.RecordObject(roomGenerator, "Remove room object");
                roomGenerator.DestroyGeneratedObjects();
            }
        
        }
    }
}
