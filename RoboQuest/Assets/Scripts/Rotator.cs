using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private Vector2 movement;
    public int jumpImpulse = 7;
    private int jumpCounter = 0;
    public GroundCheck groundCheck;
    public Rigidbody2D rb;
    public GameObject planet;
    private float playerSpeed = 10.0f;

    private Animator playerAnimator; // For later use

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = gameObject.GetComponent<Animator>();
        spriteRenderer.flipX = false;
    }

    void Update()
    {
        //if (movement != Vector2.zero)
        //{
        //    //Debug.Log(movement);
        //    if (movement.x > 0)
        //    {
        //        spriteRenderer.flipX = true;
        //    }
        //    else
        //    {
        //        spriteRenderer.flipX = false;
        //    }


        //    playerAnimator.SetBool("Run", true);
        //}

        //if (movement != Vector2.zero)
        //{
        //    spriteRenderer.flipX = movement.x > 0;
        //    playerAnimator.SetBool("Run", true);
        //}
        //else
        //{
        //    playerAnimator.SetBool("Run", false);
        //}

        //playerAnimator.SetBool("isGrounded", groundCheck.isGrounded);

        if (movement != Vector2.zero && groundCheck.isGrounded)
        {
            spriteRenderer.flipX = movement.x > 0;
            playerAnimator.SetBool("Run", true);
        }
        else
        {
            playerAnimator.SetBool("Run", false);
        }

        // Update isGrounded in Animator
        playerAnimator.SetBool("isGrounded", groundCheck.isGrounded);
    }

    void OnMove(InputValue movePosition)
    {
        //Get Player Position
        movement = movePosition.Get<Vector2>();

    }

    void OnJump()
    {
        if (groundCheck.isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            jumpCounter = 0;
            playerAnimator.SetTrigger("Jump");
            Debug.Log("Player has jumped!");
        }
        else if (jumpCounter < 2) // Allow double jump
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            jumpCounter++;
            //playerAnimator.SetTrigger("Jump");
        }
    }


    private void FixedUpdate()
    {

        //Move the planet
        planet.transform.Rotate(0, 0, movement.x * rotationSpeed * Time.deltaTime);

        //player movement
        rb.velocity = new Vector2((movement.x * playerSpeed), rb.velocity.y);

    }
}
