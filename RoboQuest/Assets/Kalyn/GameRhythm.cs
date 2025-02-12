using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameRhythm : MonoBehaviour
{// note tempo is 126.4
    public AudioSource theMusic;
    public bool startPlaynig = false;
    public static GameRhythm instance;// prevents multple instances of the scripts
    public FallingScript theBS;
    

    public int currentScore;
    public int score_perNote;

    public int currentMultiplyer;
    public int multiplyertracker;
    public int[] multiplyerThresholds;//create an array to use for levels
    public NoteObject noteController;
    public Text ScoreText ;
    public Text Multitext ;
    public GameObject sparkleEffect;

    public float beatInterval = (60 / 126.4f) * 2f; // Adjust based on song BPM
    private float nextBeatTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ScoreText.text = "Score : 0";
        currentMultiplyer = 1;
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
                noteController.StartSpawning(); //call spawn arrow- set boolean spawn = true
                //noteController.createArrow();
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

        if (startPlaynig && Time.time >= nextBeatTime) //Time.time is the gameplay time
        {
            noteController.CreateArrow(); // create arrow on beat
            nextBeatTime = Time.time + beatInterval; // Set next beat time
        }
    }

    // miss or hit notes 
    public void NoteHit() 
    {
        

        if (currentMultiplyer - 1 < multiplyerThresholds.Length)
        {
            multiplyertracker++;


            if (multiplyerThresholds[currentMultiplyer - 1] <= multiplyertracker)//
            {
                multiplyertracker = 0;

                currentMultiplyer++;

            }
        }
        Multitext.text = "Multiplier: x" + currentMultiplyer;

        Debug.Log("Note was hit ontime");
        currentScore += score_perNote * currentMultiplyer;
        ScoreText.text = "Score: " + currentScore;
    
    }

    public void NoteMissed() 
    {
        currentMultiplyer = 1;
        multiplyertracker = 0;

        Multitext.text = "Multiplier: x" + currentMultiplyer;
        Debug.Log("Note was missed");
    
    }
}
