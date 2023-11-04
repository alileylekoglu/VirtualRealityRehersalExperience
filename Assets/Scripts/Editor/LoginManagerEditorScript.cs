using UnityEditor;
using UnityEngine;
using TMPro; // Make sure to include the namespace for TextMeshPro

[CustomEditor(typeof(LoginManager))]
public class LoginManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector
        
        LoginManager loginManager = (LoginManager)target;

        // Display a text field for the player name in the editor
        loginManager.PlayerName_InputField.text = EditorGUILayout.TextField("Player Name", loginManager.PlayerName_InputField.text);

        if (GUILayout.Button("Connect Anonymously"))
        {
            loginManager.ConnectAnonymously();
        }
        if (GUILayout.Button("Connect To Photon Server"))
        {
            loginManager.ConnectToPhotonServer();
        }
    }
}