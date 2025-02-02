using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public float playerSpeed = 5.0f;
    private Vector2 movement;
    public int jumpImpulse = 7;
    private int jumpCounter = 0;
    public GroundCheck groundCheck;
    public Rigidbody2D rb;
    private GameObject planet;
    public GameObject secretExit; //cave room
    Scene scene;

    public bool allowFreeJump = false; // Toggle free jumping
    public float freeJumpForce = 7f;   //Force for free jumping

    //secret exit implementation
    private string[] correctSequence = { "W", "E", "W", "W", "E", "E", "W", "E", "W", "E" }; // Correct sequence
    private List<string> playerSequence = new List<string>(); // Tracks the player's moves


    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        rb = gameObject.GetComponent<Rigidbody2D>();
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

    }

    void OnMove(InputValue movePosition)
    {
        //Get Player Position
        movement = movePosition.Get<Vector2>();
 
    }

    void OnJump()
    {
        if (allowFreeJump)
        {
            // NEW: Free jump mode (No ground check)
            rb.velocity = Vector2.up * freeJumpForce;
        }
        else
        {
            // Trigger the Jump Animation
            if (groundCheck.isGrounded == true)
            {

                rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
                jumpCounter = 0;
                //animator.SetTrigger("Jump");
            }

            if (groundCheck.isGrounded != true && jumpCounter < 3)
            {

                rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
                jumpCounter += 1;
                // animator.SetTrigger("Jump");
            }
        }
    }

    private void FixedUpdate()
    {

        //Move the planet
        if (planet != null)
        {
            planet.transform.Rotate(0, 0, movement.x * rotationSpeed * Time.deltaTime);
        }
        //player movement
        rb.velocity = new Vector2((movement.x * playerSpeed), rb.velocity.y);

    }

    // NEW: Detect when player enters the free jump area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FreeJumpZone"))
        {
            allowFreeJump = true;
        }
    }

    //Detect when player exits the free jump area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FreeJumpZone"))
        {
            allowFreeJump = false;
        }
    }
    //Secret Exit Implementation
    void HandleInput()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame) // Left
        {
            playerSequence.Add("W");
            CheckSequence();
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame) // Right
        {
            playerSequence.Add("E");
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
                    playerSequence.Clear(); // Optionally clear the sequence after success
                    return;
                }
            }
        }


    }


}


