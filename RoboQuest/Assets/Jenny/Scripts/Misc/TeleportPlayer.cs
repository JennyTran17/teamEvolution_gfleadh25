using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public GameObject destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Only Move the Target provided to the script
        if (collision.gameObject.tag == "Player")
        {
            // Move the player to the Destination
            collision.gameObject.transform.position = destination.transform.position;
        }
        
    }
}
