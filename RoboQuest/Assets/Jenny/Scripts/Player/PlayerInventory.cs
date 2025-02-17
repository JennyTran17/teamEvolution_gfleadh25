using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    private InventoryController inventoryController;
  
   

    private void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Collectables"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            if (item != null)
            {
                //add item inventory
                bool itemAdded = inventoryController.AddItem(collision.gameObject);
                GameManager.Instance.isCollected(item.ID); //
                GameManager.Instance.RemoveDroppedItem(item.ID, item.transform.position); //
                if (itemAdded)
                {
                    Destroy(collision.gameObject);
                }

            }

        }
       
        if (collision.gameObject.name.Equals("Item"))
        {
           CollectBattery();
                
        }
       
    }

    public void CollectBattery()
    {
           
        GameManager.Instance.SaveHasBattery();
        Debug.Log("Battery collected!");
    }
}


