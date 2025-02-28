using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    public int lives = 3;

    public Text GameOverText;// Text object to display "Game Over"
    public Text WinText;// text object to display win 
    public Text LivesText;
    public bool gameOver = false;

    public GameObject Battery;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ScoreText.text = "Score : 0";
        currentMultiplyer = 1;
        LivesText.text = "Lives :" + lives;
        GameOverText.gameObject.SetActive(false);
        Debug.Log("Game Manager Started");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if(Keyboard.current.enterKey.wasPressedThisFrame)
            {
            
                SceneManager.LoadScene("Kalyn");
               
            }
            return;
            
        }
        //if (!startPlaynig)
        //{
            
        //    if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //    {
        //        GameObject instruction = GameObject.Find("instruction");
        //        instruction.SetActive(false);
        //        Debug.Log("Space key pressed!");
        //        startPlaynig = true;
        //        theBS.hasStarted = true;
        //        noteController.StartSpawning(); //call spawn arrow- set boolean spawn = true
        //        //noteController.createArrow();
        //        if (theMusic != null)
        //        {
        //            theMusic.Play();
        //            Debug.Log("Music Started");
                   
        //        }
        //        else
        //        {
        //            Debug.Log("AudioSource is not assigned!");
        //        }
        //    }
        //}

        if (startPlaynig && Time.time >= nextBeatTime) //Time.time is the gameplay time
        {
            noteController.CreateArrow(); // create arrow on beat
            nextBeatTime = Time.time + beatInterval; // Set next beat time
        }

        // Check if music is done playing (end of music)
        if (theMusic.isPlaying == false && !gameOver && theBS.hasStarted)
        {
            GameOver();
        }

        
        
        // Check if lives are 0 and game over
        if (lives <= 0 && !gameOver)
        {
            nolives();
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

    public void Ouch()
    {
        lives--;
        LivesText.text = "Lives :" + lives;
        Debug.Log("Lives: " + lives);
    }



    public void nolives()
    {
        if (lives == 0)
        {
            // Trigger the game over
            //add death animation trigger
            player.GetComponent<Animator>().SetTrigger("Death");

            GameOver();
        }
    }

    // Game over 
    private void GameOver()
    {
        gameOver = true;
        if (theMusic != null)
        {
            theMusic.Stop(); // Stop the music
        }

        if (gameOver == true)
        {
            if (currentScore >= 2000)
            {// show win  message
                WinText.gameObject.SetActive(true);
                WinText.text = "Power cell Crafted!\nFinal Score: " + currentScore;

                Instantiate(Battery, transform.position, Quaternion.identity);
            }
            else if (currentScore < 2000)
            {
                // Show "Game Over" message
                GameOverText.gameObject.SetActive(true);
                GameOverText.text = "Game Over!\nFinal Score: " + currentScore + "\n Press Enter to restart";
                
            }

        }


        //Time.timeScale = 0; // freeze the game
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            if (!startPlaynig)
            {


                GameObject instruction = GameObject.Find("instruction");
                instruction.SetActive(false);
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
    }


}
