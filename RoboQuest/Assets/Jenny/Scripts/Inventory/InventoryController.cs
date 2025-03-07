
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    public GameObject inventoryPanel;
    public GameObject slotPrefab; 
    public int slotCount;
    public GameObject[] itemPrefabs;

    //public AudioSource itemCollectedAudio;
  

    private void Start()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();
    }

    private void Update()
    {
        if (inventoryPanel == null)
        {
            inventoryPanel = GameObject.Find("grid");
        }
    }
    //if there is no file
    public void initialSetUp()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);

        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        //Look for empty slot
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if(slot != null && slot.currentItem == null)
            {
                Debug.Log($"Before Instantiate: {itemPrefab.transform.localScale}");
                GameObject newItem = Instantiate(itemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                //Debug.Log($"After Instantiate: {newItem.transform.localScale}");
                //newItem.transform.localScale = new Vector3(0.8f, 1f, 1f); 
                slot.currentItem = newItem;

                //itemCollectedAudio.Play();

                return true;
            }
        }
        Debug.Log("Inventory is full");
        return false;
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                if (item != null)
                {
                    invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
                    Debug.Log($" Saved Item: ID {item.ID} in Slot {slotTransform.GetSiblingIndex()}");
                }
                else
                {
                    Debug.LogWarning($" Slot {slotTransform.GetSiblingIndex()} has an object but no Item component!");
                }
            }
            else
            {
                Debug.Log($" Slot {slotTransform.GetSiblingIndex()} is empty, skipping...");
            }
        }

        Debug.Log($" Final Saved Data: " + invData);
        return invData;
    }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
      
        // Clear existing inventory to avoid duplicates
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Create new slots
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        Debug.Log($" Created {slotCount} empty slots.");

        // Populate slots with saved items
        foreach (InventorySaveData data in inventorySaveData)
        {
            Debug.Log($" Checking Slot {data.slotIndex} for Item ID {data.itemID}...");

            if (data.slotIndex < slotCount)
            {
                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);

                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    Debug.Log(itemPrefab);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item; 

                    Debug.Log($" Loaded Item ID {data.itemID} into Slot {data.slotIndex}");

                }
                else
                {
                    Debug.LogError($" Could not find prefab for Item ID {data.itemID}");
                }
            }
           
        }
    }

    public void SetDropItem(List<DropItemController> dropItem)
    {
        foreach(DropItemController itemData in dropItem)
        {
            GameObject itemPrefab = itemDictionary.GetItemPrefab(itemData.itemID);
            if (itemPrefab != null)
            {
                GameObject itemObject = Instantiate(itemPrefab, itemData.position, Quaternion.identity);
                Debug.Log(itemPrefab);
                //Item itemComponent = itemObject.GetComponent<Item>();
                //itemComponent.ID = itemData.itemID;
            }

        }
    }


}