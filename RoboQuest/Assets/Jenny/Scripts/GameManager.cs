using System.Collections.Generic;
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
    private SaveData saveData = new SaveData();//
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

        //SaveData saveData = new SaveData
        //{
        //    playerPosition = player.transform.position,
        //    inventorySaveData = inventoryController.GetInventoryItems()

        //};

        saveData.playerPosition = player.transform.position;
        saveData.inventorySaveData = inventoryController.GetInventoryItems();


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
            this.saveData.collectedObjID = saveData.collectedObjID;// ensure loaded data is assigned properly
            RemoveCollectedItemsFromScene(saveData.inventorySaveData);  //check if json file list have the item ID in the inventory, delete the game object in hierarchy

            player.transform.position = saveData.playerPosition;

            inventoryController.SetInventoryItems(saveData.inventorySaveData);
            inventoryController.SetDropItem(saveData.droppedItems);
            
        }
        else
        {
            inventoryController.initialSetUp();
            SaveGame();
        }

    }

    private void RemoveCollectedItemsFromScene(List<InventorySaveData> savedInventory)
    {
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectables");

        foreach (GameObject obj in collectables)
        {
            Item item = obj.GetComponent<Item>();
            if (item != null )
            {
                //foreach (InventorySaveData savedItem in savedInventory)
                //{
                //    if (item.ID == savedItem.itemID)
                //    {
                //        Debug.Log($"Destroying collected item: {obj.name} (ID: {item.ID})");
                //        Destroy(obj);
                //        break; // Stop checking once a match is found
                //    }
                //}

                foreach (int id in saveData.collectedObjID)
                {
                    if (item.ID == id)
                    {
                        Debug.Log($"Destroying collected item: {obj.name} (ID: {item.ID})");
                        Destroy(obj);
                        break; // Stop checking once a match is found
                    }
                }

            }
        }
    }

    //ensure the item collected is never respawn to the world once collected
    public void isCollected(int id)
    {
        if(!saveData.collectedObjID.Contains(id))
        {
            saveData.collectedObjID.Add(id);
            SaveGame();
        }
    }

    public void SaveDroppedItem(int itemID, Vector3 position)
    {
        DropItemController newItem = new DropItemController(itemID, position);
        saveData.droppedItems.Add(newItem);
        SaveGame();
    }


    public void RemoveDroppedItem(int itemID, Vector3 position)
    {
        saveData.droppedItems.RemoveAll(item => item.itemID == itemID && item.position == position);
        SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame(); // Save before quitting
    }
}
