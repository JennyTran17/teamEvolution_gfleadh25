using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public float playerSpeed = 5.0f;
    public Vector2 movement;
    public int jumpImpulse = 7;
    private int jumpCounter = 0;
    public GroundCheck groundCheck;
    public Rigidbody2D rb;
    private GameObject planet;
    public GameObject secretExit; //cave room
    public Scene scene;

    private Animator playerAnimator; // For later use

    public AudioSource walkingAudio; // Walking audio source
    public AudioSource jumpAudio; // Jumping audio source

    private SpriteRenderer spriteRenderer;

    //secret exit implementation
    private string[] correctSequence = { "R", "R", "U", "L", "U", "R", "L", "L", "U", "R" }; // Correct sequence
    private List<string> playerSequence = new List<string>(); // Tracks the player's moves


    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = gameObject.GetComponent<Animator>();
        walkingAudio = gameObject.GetComponent<AudioSource>();
        spriteRenderer.flipX = false;

        if (planet == null && scene.name.Equals("Main Level"))
        {
            planet = GameObject.FindGameObjectWithTag("Planet");
        }
        if(secretExit == null && scene.name.Equals("Cave"))
        {
            secretExit = GameObject.FindGameObjectWithTag("SecretExit");
            secretExit.SetActive(false);
        
        }

        

    }
    private void Update()
    {
        if (scene.name.Equals("Cave"))
        {
            HandleInput();
        }

        if (movement != Vector2.zero)
        {
            spriteRenderer.flipX = movement.x > 0;

            if(groundCheck.isGrounded)
            {
                playerAnimator.SetBool("Run", true);
                walkingAudio.enabled = true;
                Debug.Log("Walking sound");
            }
        }
        else
        {
            playerAnimator.SetBool("Run", false);
            walkingAudio.enabled = false;
            
        }
    }

    void OnMove(InputValue movePosition)
    {
        //Get Player Position
        movement = movePosition.Get<Vector2>();
 
    }

    void OnJump()
    {
        playerAnimator.SetTrigger("Jump");

        // Trigger the Jump Animation
        if (groundCheck.isGrounded == true)
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            jumpCounter = 0;
            Debug.Log("Player has jumped!");
        }

        if (groundCheck.isGrounded != true && jumpCounter < 3)
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            jumpCounter += 1;
        }

    }

    private void FixedUpdate()
    {

        //Move the planet
        if (planet != null)
        {
            planet.transform.Rotate(0, 0, movement.x * rotationSpeed * Time.deltaTime);
            GameManager.Instance.SavePlanetPosition(planet.transform.rotation);
        }
        //player movement
        rb.velocity = new Vector2((movement.x * playerSpeed), rb.velocity.y);

    }

    //Secret Exit Implementation
    void HandleInput()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame) // Left
        {
            playerSequence.Add("L");
            CheckSequence();
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame) // Right
        {
            playerSequence.Add("R");
            CheckSequence();
    }
        else if (Keyboard.current.upArrowKey.wasPressedThisFrame) // Up
        {
            playerSequence.Add("U");
            CheckSequence();
        }
    }

    void CheckSequence()
    {
        // Index to track progress through the correct sequence
        int correctSequenceIndex = 0;

        // Loop through the player's sequence
        for (int i = 0; i < playerSequence.Count; i++)
        {
            // If the current player input matches the expected input in the correct sequence
            if (playerSequence[i] == correctSequence[correctSequenceIndex])
            {
                correctSequenceIndex++; // Move to the next expected input in the correct sequence

                // If the entire correct sequence has been matched
                if (correctSequenceIndex == correctSequence.Length)
                {
                    Debug.Log("Correct sequence! Exit unlocked.");
                    secretExit.SetActive(true);
                    if(CinemachineShake.instance != null)
                    {
                        CinemachineShake.instance.ShakeCamera(3f, 2f);
                    }
                   
                    playerSequence.Clear(); // Optionally clear the sequence after success
                    return;
                }
            }
        }
    }

    public void JumpSoundTrigger()
    {
        jumpAudio.Play();
    }
}


