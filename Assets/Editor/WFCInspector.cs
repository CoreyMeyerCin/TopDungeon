using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Test))]
public class WFCInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Test myScript = (Test)target;
        if(GUILayout.Button("Create tilemap"))
        {
            Debug.Log("Going to CreateWFC()");
            myScript.CreateWFC();
            Debug.Log("Going to CreateTilemap()");
            myScript.CreateTilemap();
        }
        if(GUILayout.Button("Save tilemap"))
        {
            myScript.SaveTilemap();
        }
    }
}
