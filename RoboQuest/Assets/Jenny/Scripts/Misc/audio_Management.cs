using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_Management : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
    }


}
