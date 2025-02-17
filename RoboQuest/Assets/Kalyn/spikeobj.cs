using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeobj : MonoBehaviour
{
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if the object collides with the buttons make them pressable
        if (other.gameObject.tag == "Spike")
        {

            Destroy(other.gameObject);

            Debug.Log("hit by spike");

            GameRhythm.instance.Ouch();



        }
    }
}
