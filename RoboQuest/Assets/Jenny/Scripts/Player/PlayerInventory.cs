using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasBattery = false;
    public List<GameObject> inventory = new List<GameObject>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Collectables"))
        {
            inventory.Add(collision.gameObject);
            if(collision.gameObject.name.Equals("Battery"))
            {
                hasBattery = true;
            }
            Debug.Log(collision.gameObject.name + " is added to inventory");
            Destroy(collision.gameObject);

        }
    }

    public void CollectBattery()
    {
        hasBattery = true;
        Debug.Log("Battery collected!");
    }
}