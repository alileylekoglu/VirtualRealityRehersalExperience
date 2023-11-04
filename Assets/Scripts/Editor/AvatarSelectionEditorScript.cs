using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AvatarSelectionManager))]
public class AvatarSelectionEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Draws the default inspector

        AvatarSelectionManager avatarSelectionManager = (AvatarSelectionManager)target;

        // Add a button to activate the avatar selection platform
        if (GUILayout.Button("Activate Avatar Selection Platform"))
        {
            avatarSelectionManager.ActivateAvatarSelectionPlatform();
        }

        // Add a button to deactivate the avatar selection platform
        if (GUILayout.Button("Deactivate Avatar Selection Platform"))
        {
            avatarSelectionManager.DeactivateAvatarSelectionPlatform();
        }

        // Add a button for selecting the next avatar
        if (GUILayout.Button("Next Avatar"))
        {
            avatarSelectionManager.NextAvatar();
        }

        // Add a button for selecting the previous avatar
        if (GUILayout.Button("Previous Avatar"))
        {
            avatarSelectionManager.PreviousAvatar();
        }
    }
}