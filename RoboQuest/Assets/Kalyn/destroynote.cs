using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNote : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OntriggerEnter2D(Collision2D other)
    {
        // Check if the object colliding has the tags of the notes
        if (other.gameObject.CompareTag("bluearrow") || other.gameObject.CompareTag("redarrow")
            || other.gameObject.CompareTag("yellowarrow") || other.gameObject.CompareTag("greenarrow"))
        {
            // Destroy the note object
            Destroy(other.gameObject);

            // Optionally, log for debugging
            Debug.Log("Note Destroyed: " + other.gameObject.tag);

            // Call the method to indicate that a note was missed
            GameRhythm.instance.NoteMissed();
        }
    }
}
