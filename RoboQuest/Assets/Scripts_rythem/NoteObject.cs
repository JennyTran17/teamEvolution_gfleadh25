using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    public GameObject hitEffect;


    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnCollisionEnter2D(Collision2D other)
    {//if the object collides with the buttons make them pressable
        if (other.gameObject.tag == "bluearrow" || other.gameObject.tag == "redarrow" || other.gameObject.tag == "yellowarrow" || other.gameObject.tag == "greenarrow")
        {
            //canbepressed = true;
            Destroy(other.gameObject);
            //gameObject.SetActive(false);
            Debug.Log("Delete blue arrow");
            GameRhythm.instance.NoteHit();

            Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

        }
        
    }
    private void OnCollisionExit2D(Collider2D other)
    {
        if (other.tag == "bluearrow" || other.tag == "redarrow" || other.tag == "yellowarrow" || other.tag == "greenarrow")
        {
            
            GameRhythm.instance.NoteMissed();
        }
        
       
    }
}

