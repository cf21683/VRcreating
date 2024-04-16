using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using System.IO;

[InitializeOnLoad]
public class AutoSaveScene
{
    // use EditorPref to get bool variable to control the save function
    private static bool autoSaveEnabled ;
    private const string menuPath = "Tools/AutoSave";
    static AutoSaveScene(){
        autoSaveEnabled = EditorPrefs.GetBool("AutoSaveEnabled", true);
        EditorApplication.playModeStateChanged += delaySave;
        CreateFolder();
        Menu.SetChecked(menuPath, autoSaveEnabled);
    }

    private static void delaySave(PlayModeStateChange state){
        if(state == PlayModeStateChange.ExitingPlayMode && autoSaveEnabled){
            EditorApplication.delayCall += SaveScene;   // Use delayed subscriptions to ensure that methods are not called in play mode
        }
    }
    private static void SaveScene(){
       
        string autoSaveScenePath = "Assets/AutoSave";
        string scenePath = SceneManager.GetActiveScene().path;
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string autoSavePath = Path.Combine(autoSaveScenePath,timeStamp + ".unity");

        if(!Directory.Exists(autoSaveScenePath)){
            Directory.CreateDirectory(autoSaveScenePath);
        }

        EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), autoSavePath);

        
        EditorApplication.delayCall -= SaveScene;
    }

    [MenuItem(menuPath)]
    private static void AutoSave(){
        autoSaveEnabled = !autoSaveEnabled;
        EditorPrefs.SetBool("AutoSaveEnabled", autoSaveEnabled);
        Menu.SetChecked(menuPath, autoSaveEnabled);
    }

    private static void CreateFolder(){
        string autoSaveScenePath = "Assets/AutoSave";
        if (!Directory.Exists(autoSaveScenePath))
        {
            Directory.CreateDirectory(autoSaveScenePath);
        }
    
}
}
