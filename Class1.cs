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
            // Create the window
            GetWindow<RageQuiter>("RageQuiter");
        }

        private void OnGUI()
        {
            // Create the label to go above the button
            GUILayout.Label("Press to RAGE QUIT!", EditorStyles.boldLabel);

            // Create the rage quit button
            if (GUILayout.Button("RAGE QUIT!"))
            {
                // Launch a confirmation warning to the user
                bool confirm = EditorUtility.DisplayDialog("Confirm Rage Quit", "Are you sure you want to rage quit your Unity project? All files will be purged.", "Yes", "No");

                // If the user confirms
                if (confirm)
                {
                    // Call the function that deletes all the assets
                    DeleteAllFilesInAssetsFolder();

                    // Wish the user good luck in the console
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

            // Iterate through all the asset files
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
                    // Print a warning in the console if any issues arrise
                    Debug.LogWarning("Failed to delete file: " + filePath + ". Error: " + ex.Message);
                }
            }

            // Refresh the AssetDatabase to reflect changes in the Unity Editor
            AssetDatabase.Refresh();
        }

        // Function to check if a file is in the editor folder which is where the script is located
        private bool IsFileWithinEditorFolder(string filePath)
        {
            // Path to the Editor folder
            string editorFolderPath = Path.Combine(Application.dataPath, "Editor");

            // Check if the file is within the Editor folder
            return filePath.StartsWith(editorFolderPath);
        }
    }
}
