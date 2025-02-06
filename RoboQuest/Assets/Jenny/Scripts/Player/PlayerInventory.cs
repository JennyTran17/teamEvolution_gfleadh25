using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private InventoryController inventoryController;
    public bool hasBattery = false;


    private void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Collectables"))
        {
            if (collision.gameObject.name.Equals("Battery"))
            {
                hasBattery = true;
            }

            Item item = collision.gameObject.GetComponent<Item>();
            if (item != null)
            {
                //add item inventory
                bool itemAdded = inventoryController.AddItem(collision.gameObject);
                if (itemAdded)
                {
                    Destroy(collision.gameObject);
                }

            }

        }
    }

    public void CollectBattery()
    {
        hasBattery = true;
        Debug.Log("Battery collected!");
    }
}


