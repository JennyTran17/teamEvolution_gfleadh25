using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAudio : MonoBehaviour
{
    public AudioSource waterAudio;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            waterAudio.Play();
        }

    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            waterAudio.Play();
        }

    }
}
