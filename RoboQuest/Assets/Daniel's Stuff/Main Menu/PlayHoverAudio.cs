using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHoverAudio : MonoBehaviour
{
    public GameObject audioObj;

    public void DropAudio()
    {
        Instantiate(audioObj, transform.position, transform.rotation);
    }
}
