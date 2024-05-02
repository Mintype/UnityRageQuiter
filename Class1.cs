using UnityEditor;
using UnityEngine;
using System.IO;

namespace UnityRageQuiter
{
    public class RageQuiter : EditorWindow
    {

        [MenuItem("Window/RageQuiter")]
        public static void ShowWindow()
        {
            GetWindow<RageQuiter>("RageQuiter");
        }

        private void OnGUI()
        {
            GUILayout.Label("Press to RAGE QUIT!", EditorStyles.boldLabel);

            if (GUILayout.Button("RAGE QUIT!"))
            {
                bool confirm = EditorUtility.DisplayDialog("Confirm Rage Quit", "Are you sure you want to rage quit your Unity project? All files will be purged.", "Yes", "No");

                if (confirm)
                {
                    DeleteAllFilesInAssetsFolder();
                    Debug.Log("Good luck next time.");
                }
            }
        }

        private void DeleteAllFilesInAssetsFolder()
        {
            // Get the path to the Assets folder
            string assetsPath = Application.dataPath;

            // Get all files within the Assets folder and its subdirectories
            string[] allFiles = Directory.GetFiles(assetsPath, "*", SearchOption.AllDirectories);

            foreach (string filePath in allFiles)
            {
                // Skip the script's own file
                if (IsFileWithinEditorFolder(filePath)) 
                    continue;

                try
                {
                    // Try to delete each file
                    File.Delete(filePath);
                    Debug.Log("Deleted file: " + filePath);
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning("Failed to delete file: " + filePath + ". Error: " + ex.Message);
                }
            }

            // Refresh the AssetDatabase to reflect changes in the Unity Editor
            AssetDatabase.Refresh();
        }

        private bool IsFileWithinEditorFolder(string filePath)
        {
            string editorFolderPath = Path.Combine(Application.dataPath, "Editor"); // Path to the Editor folder
            return filePath.StartsWith(editorFolderPath); // Check if the file is within the Editor folder
        }
    }
}
