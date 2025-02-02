using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private string saveLocation;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        // Automatically load game data when the game starts
        LoadGame();
    }

    // Async Save Method
    public async void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("SaveGame: Player not found, skipping save.");
            return; // Prevents the error when stopping play mode
        }

        SaveData saveData = new SaveData
        {
            playerPosition = player.transform.position
        };

        string json = JsonUtility.ToJson(saveData);
        await File.WriteAllTextAsync(saveLocation, json);

        Debug.Log("Game Saved Successfully!");
    }


    // Async Load Method
    public async void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            string json = await File.ReadAllTextAsync(saveLocation);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
        }
    }

    // Auto-save when quitting
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    // Auto-save when scene changes
    private void OnDestroy()
    {
        SaveGame();
    }

    private void Update()
    {
        Debug.Log(Application.persistentDataPath);
    }
}
