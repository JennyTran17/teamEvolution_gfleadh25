
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public List<Item> itemPrefabs;
    private Dictionary<int, GameObject> itemDictionary;

    private void Awake()
    {
        itemDictionary = new Dictionary<int, GameObject>();
        //AutoIncrementIds
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            if (itemPrefabs[i] != null)
            {
                itemPrefabs[i].ID = i + 1;
            }
        }
          
        foreach (Item item in itemPrefabs)
        {
             itemDictionary[item.ID] = item.gameObject;
                
        }
            
        
    }
      
    public GameObject GetItemPrefab(int itemID)
    {
        if (itemDictionary.TryGetValue(itemID, out GameObject prefab))
        {
            return prefab;
        }
        else
        {
            Debug.LogError($"Item with ID {itemID} not found in dictionary!");
            return null;
        }
    }
    
}
