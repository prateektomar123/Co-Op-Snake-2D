using UnityEngine;
using UnityEditor;
using System.IO;

public class FolderCreator : EditorWindow
{
    private string folderPath = "";
    private string[] folderNames;
    private int folderCount = 5;

    [MenuItem("Tools/Create Folders From Array")]
    public static void ShowWindow()
    {
        GetWindow<FolderCreator>("Folder Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Folders From String Array", EditorStyles.boldLabel);

        if (GUILayout.Button("Select Folder"))
        {
            folderPath = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
        }

        GUILayout.Label("Selected Folder: " + folderPath);

        folderCount = EditorGUILayout.IntField("Number of Folders:", folderCount);

        if (folderNames == null || folderNames.Length != folderCount)
        {
            folderNames = new string[folderCount];
        }

        for (int i = 0; i < folderCount; i++)
        {
            folderNames[i] = EditorGUILayout.TextField($"Folder {i + 1} Name:", folderNames[i]);
        }

        if (GUILayout.Button("Create Folders"))
        {
            if (!string.IsNullOrEmpty(folderPath) && folderNames != null)
            {
                CreateFolders();
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please select a folder and provide valid folder names.", "OK");
            }
        }
    }

    private void CreateFolders()
    {
        string relativePath = "Assets" + folderPath.Substring(Application.dataPath.Length);
        
        foreach (var folderName in folderNames)
        {
            if (!string.IsNullOrEmpty(folderName))
            {
                string newFolderPath = Path.Combine(relativePath, folderName);
                if (!AssetDatabase.IsValidFolder(newFolderPath))
                {
                    AssetDatabase.CreateFolder(relativePath, folderName);
                }
            }
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Success", "Folders created successfully!", "OK");
    }
}
