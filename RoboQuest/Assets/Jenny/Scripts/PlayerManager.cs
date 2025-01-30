using System.Collections;
using System.Collections.Generic;
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
    Scene scene;

    public bool allowFreeJump = false; // Toggle free jumping
    public float freeJumpForce = 7f;   //Force for free jumping

    public ScriptableObj playerData;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(planet == null && scene.name.Equals("Main Level") )
        {
            planet = GameObject.FindGameObjectWithTag("Planet");
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

    // NEW: Detect when player exits the free jump area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FreeJumpZone"))
        {
            allowFreeJump = false;
        }
    }

}
