
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    public GameObject inventoryPanel;
    public GameObject slotPrefab; 
    public int slotCount;
    public GameObject[] itemPrefabs;

    void Awake()
    {
        //itemDictionary = FindObjectOfType<ItemDictionary>();
        //for (int i = 0; i < slotCount; i++)
        //{
        //    Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
        //    if (i < itemPrefabs.Length)
        //    {
        //        GameObject item = Instantiate(itemPrefabs[i], slot.transform);
        //        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //        slot.currentItem = item;
        //    }
        //}
    }
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


}