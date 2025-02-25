using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAudio : MonoBehaviour
{
    public AudioSource npcAudio;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            npcAudio.Play();
        }
        
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            npcAudio.Stop();
        }

    }
}
