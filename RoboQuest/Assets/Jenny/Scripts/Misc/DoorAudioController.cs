using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudioController : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    public void Open()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void WallHit()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
}
