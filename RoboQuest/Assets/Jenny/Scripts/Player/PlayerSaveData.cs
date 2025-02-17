using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveData : MonoBehaviour
{
    public static PlayerSaveData Instance;
    private string savePath;

    private Dictionary<string, Vector3> scenePositions = new Dictionary<string, Vector3>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "playerPosition.json");
           LoadPositionData();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Save player position before changing scene
    public void SavePlayerPosition(Vector3 position)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        scenePositions[currentScene] = position;
        SavePositionData();
    }

    // Load the saved position for the current scene
    public Vector3 GetSavedPosition()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (scenePositions.TryGetValue(currentScene, out Vector3 savedPos))
        {
            return savedPos;
        }
        return Vector3.zero; // Default position if no data found
    }

    private void SavePositionData()
    {
        PositionData data = new PositionData(scenePositions);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public void LoadPositionData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PositionData data = JsonUtility.FromJson<PositionData>(json);
            scenePositions = data.ToDictionary(); //convert list back to dictionary
        }
    }

    [System.Serializable]
    private class PositionData
    {
        public List<string> sceneNames = new List<string>();
        public List<Vector3> positions = new List<Vector3>();

        public PositionData(Dictionary<string, Vector3> dict)
        {
            foreach (var entry in dict)
            {
                sceneNames.Add(entry.Key);
                positions.Add(entry.Value);
            }
        }

        public Dictionary<string, Vector3> ToDictionary()
        {
            Dictionary<string, Vector3> dict = new Dictionary<string, Vector3>();
            for (int i = 0; i < sceneNames.Count; i++)
            {
                dict[sceneNames[i]] = positions[i];
            }
            return dict;
        }
    }
}
