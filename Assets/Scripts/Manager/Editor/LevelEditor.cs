using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(LeveLDataManager))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var script = (LeveLDataManager)target;
        if (GUILayout.Button("Save Map"))
        {
            script.SaveLevel();
        }
        if (GUILayout.Button("Clear Map"))
        {
            script.ClearLevel();
        }
        if (GUILayout.Button("Load Map"))
        {
            script.LoadLevel();
        }
        if(GUILayout.Button("Load Level Testing"))
        {
            script.LoadLevelTesting();
        }
    }
}
