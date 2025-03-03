using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private string saveLocation;
    private InventoryController inventoryController;
    private GameObject player;
    public SaveData saveData = new SaveData();//
    private PlayerInventory playerInventory;
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
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DelayedLoadGame());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(ApplyPlayerPosition());
        RemoveCollectedItemsFromScene(saveData.inventorySaveData);

    }

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SaveGame();
            SceneManager.LoadScene("Main Menu");
            
            
        }
    }

    public async void SaveGame()
    {
        if (inventoryController == null)
        {
            Debug.Log("SaveGame failed: Missing player or inventory reference!");
            return;
        }

        
        saveData.inventorySaveData = inventoryController.GetInventoryItems();
       
        if (player != null)
        {
            PlayerSaveData.Instance.SavePlayerPosition(player.transform.position);
        }



        string json = JsonUtility.ToJson(saveData);
        await File.WriteAllTextAsync(saveLocation, json);
        Debug.Log("Game Saved Successfully!");
        Debug.Log(saveLocation);
    }

    public async void LoadGame() 
    {
       
        if (File.Exists(saveLocation))
        {
            string json = await File.ReadAllTextAsync(saveLocation);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            this.saveData.collectedObjID = saveData.collectedObjID;// ensure loaded data is assigned properly
            //RemoveCollectedItemsFromScene(saveData.inventorySaveData);  //check if json file list have the item ID in the inventory, delete the game object in hierarchy



            inventoryController.SetInventoryItems(saveData.inventorySaveData);
            inventoryController.SetDropItem(saveData.droppedItems);
            this.saveData.hasBattery = saveData.hasBattery;
            this.saveData.hasWire = saveData.hasWire;
            this.saveData.hasFuel = saveData.hasFuel;
            this.saveData.completeCP = saveData.completeCP;

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
        GameObject inventoryObject = GameObject.Find("Inventory");

        foreach (GameObject obj in collectables)
        {
            
            Item item = obj.GetComponent<Item>();
            if (item != null )
            {
                
                foreach (int id in saveData.collectedObjID)
                {
                    if (item.ID == id && !obj.transform.IsChildOf(inventoryObject.transform))
                    {
                        Debug.Log($"Destroying collected item: {obj.name} (ID: {item.ID})");
                        Destroy(obj);
                        break; // Stop checking once a match is found
                    }
                    else
                    {
                        continue;
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

    private IEnumerator DelayedLoadGame()
    {
        yield return new WaitForSeconds(0.1f); // Allow scene objects to initialize

        player = GameObject.FindGameObjectWithTag("Player");
        inventoryController = FindObjectOfType<InventoryController>();
        playerInventory = FindObjectOfType<PlayerInventory>();

        if (player == null) Debug.Log("Player not found in new scene!");
        if (inventoryController == null) Debug.LogWarning("InventoryController not found in new scene!");

        player = GameObject.FindGameObjectWithTag("Player");

       

        LoadGame();
    }

   

    private IEnumerator ApplyPlayerPosition()
    {
        yield return new WaitForEndOfFrame(); // Wait for all objects to be initialized

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 savedPosition = PlayerSaveData.Instance.GetSavedPosition();
            if (savedPosition != Vector3.zero)
            {
                player.transform.position = savedPosition;
                Debug.Log($"Applied saved position: {savedPosition}");
            }
            else
            {
                Debug.Log("No saved position found for this scene.");
            }
        }
        else
        {
            Debug.LogWarning("Player not found in the scene!");
        }
    }


    public void SavePlanetPosition(Quaternion rotation)
    {
        saveData.planetPosition = rotation;
        SaveGame();
    }

    public void SaveHasBattery()
    {
        saveData.hasBattery = true;
        SaveGame();
    }
    public void SaveHasWire()
    {
        saveData.hasWire = true;

        SaveGame();
    }
    public void SaveHasFuel()
    {
        saveData.hasFuel = true;
        SaveGame();
    }
    //public void SaveHasSpareParts()
    //{
    //    saveData.hasSpareParts = true;
    //    SaveGame();
    //}
    public void completeCP()
    {
        saveData.completeCP = true;
        SaveGame();
    }
    public void completeFuel()
    {
        saveData.completeFuel = true;
        SaveGame();
    }

    public void completeConnLevel1()
    {
        saveData.completeConnLvl1 = true;
        SaveGame();
    }
    public void completeConnLevel2()
    {
        saveData.completeConnLvl2 = true;
        SaveGame();
    }
    public void completeConnLevel3()
    {
        saveData.completeConnLvl3 = true;
        SaveGame();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
