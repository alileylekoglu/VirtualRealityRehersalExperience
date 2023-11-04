using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector
        EditorGUILayout.HelpBox("This script is responsible for creating and joining rooms", MessageType.Info);
        RoomManager roomManager = (RoomManager)target;
        
        if (GUILayout.Button("Join Game Room"))
        {
           roomManager.OnEnteredButtonClicked_Game();
        }

        if (GUILayout.Button("Join Practice Room"))
        {
            roomManager.OnEnteredButtonClicked_Practice();
        }
        
    }
         
}
