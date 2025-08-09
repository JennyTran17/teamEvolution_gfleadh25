using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAudio : MonoBehaviour
{
    public AudioSource waterAudio;
    public AudioSource waterAudio2;
    public PlayerManager playerManager;
    bool inWater;

    public GameObject splash;
    private void Start()
    {
        playerManager = GameObject.FindFirstObjectByType<PlayerManager>();
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            Vector3 offset = new Vector3(0f, -0.3f, 0f);
            Instantiate(splash, obj.transform.position, Quaternion.identity);
            waterAudio.Play();
            Debug.Log("Player speed changed in water");
            playerManager.playerSpeed = 2.5f;
            //playerManager.rb.drag = 10;
            waterAudio2.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        { 
            Vector3 offset = new Vector3(0f, -0.3f, 0f);
            Instantiate(splash, obj.transform.position + offset, Quaternion.identity);

            waterAudio.Play();
            playerManager.playerSpeed = 6f;
            //playerManager.rb.drag = 0;
            waterAudio2.Stop();
        }
    }
}
