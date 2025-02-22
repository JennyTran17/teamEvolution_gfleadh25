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

            if (item.ID == 1)
            {
                Debug.Log("base prefab");

            }
            else if (item.ID == 2)
            {
                GameManager.Instance.SaveHasBattery();

            }
            else if (item.ID == 3)
            {
                GameManager.Instance.SaveHasWire();

            }
            else if (item.ID == 4)
            {
                GameManager.Instance.SaveHasFuel();

            }
            //else if (collision.gameObject.name.Equals("Spare Parts"))
            //{
            //    GameManager.Instance.SaveHasSpareParts();

            //}

        }
       
        


    }

    
}


