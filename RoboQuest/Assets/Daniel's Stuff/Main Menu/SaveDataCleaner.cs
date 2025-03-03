using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataCleaner : MonoBehaviour
{
    //refresh game everytime player press Play Button in main menu
    //delete game manager, player save manager, inventory game object 
    void Start()
    {
        CleanupPersistentObjects();
        DeleteAllSaveFiles();
    }

    private void CleanupPersistentObjects()
    {
        DeleteIfExists("GameManager");
        DeleteIfExists("Inventory");
        DeleteIfExists("PlayerSaveManager");
    }

    private void DeleteIfExists(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            Destroy(obj);
            Debug.Log($"{objectName} was found and destroyed.");
        }
    }

    private void DeleteAllSaveFiles()
    {
        string folderPath = Application.persistentDataPath;

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                    Debug.Log($"Deleted file: {file}");
                }
                catch (IOException e)
                {
                    Debug.LogError($"Failed to delete file: {file}. Error: {e.Message}");
                }
            }
            Debug.Log("All save files deleted.");
        }
        else
        {
            Debug.LogWarning("Save folder does not exist.");
        }
    }
}
