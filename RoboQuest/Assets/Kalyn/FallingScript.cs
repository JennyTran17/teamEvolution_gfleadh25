using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingScript : MonoBehaviour
{
    public float beatTempo = 2.0f;
    public bool hasStarted = false;
    public GameObject sparkleEffect;

    void Start()
    {
        float bpm = 126.4f; //beat temp
        beatTempo = (bpm / 60f) * 4f; // beat per sec * (any number to controll how fast or slow)
    }

    void Update()
    {
        if (hasStarted)
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }

    public void StartFalling()
    {
        hasStarted = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            GameRhythm.instance.NoteMissed();
            Destroy(gameObject);
        }
        else
        {
            GameRhythm.instance.NoteHit();
            Instantiate(sparkleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
