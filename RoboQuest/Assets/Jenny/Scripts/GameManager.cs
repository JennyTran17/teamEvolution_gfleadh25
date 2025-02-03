using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private string saveLocation;
    private InventoryController inventoryController;
    private GameObject player;

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

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    //private void Start()
    //{
    //    inventoryController = FindObjectOfType<InventoryController>();
    //    player = GameObject.FindGameObjectWithTag("Player");
    //    LoadGame();
    //}

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        player = GameObject.FindGameObjectWithTag("Player");
        inventoryController = FindObjectOfType<InventoryController>();

        if (player == null) Debug.LogWarning("Player not found in new scene!");
        if (inventoryController == null) Debug.LogWarning("InventoryController not found in new scene!");

        LoadGame(); // Load the saved data into the new objects
    }

    public async void SaveGame()
    {
        if (player == null || inventoryController == null)
        {
            Debug.LogError("SaveGame failed: Missing player or inventory reference!");
            return;
        }

        SaveData saveData = new SaveData
        {
            playerPosition = player.transform.position,
            inventorySaveData = inventoryController.GetInventoryItems()
        };

        string json = JsonUtility.ToJson(saveData);
        await File.WriteAllTextAsync(saveLocation, json);
        Debug.Log("Game Saved Successfully!");
    }

    public async void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            string json = await File.ReadAllTextAsync(saveLocation);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            player.transform.position = saveData.playerPosition;

            inventoryController.SetInventoryItems(saveData.inventorySaveData);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame(); // Save before quitting
    }
}
