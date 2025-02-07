using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameRhythm : MonoBehaviour
{// note tempo is 126.4
    public AudioSource theMusic;
    public bool startPlaynig = false;
    public static GameRhythm instance;// prevents multple instances of the scripts
    public FallingScript theBS;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Debug.Log("Game Manager Started");
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaynig)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("Space key pressed!");
                startPlaynig = true;
                theBS.hasStarted = true;

                if (theMusic != null)
                {
                    theMusic.Play();
                    Debug.Log("Music Started"); 
                }
                else
                {
                    Debug.Log("AudioSource is not assigned!");
                }
            }
        }
    }

    // miss or hit notes 
    public void NoteHit() 
    {
        Debug.Log("Note was hit ontime");
    
    }

    public void NoteMissed() 
    {
        Debug.Log("Note was missed");
    
    }
}
