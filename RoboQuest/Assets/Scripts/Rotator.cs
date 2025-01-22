using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private float playerSpeed = 10.0f;
    private Vector2 movement;
    public int jumpImpulse = 7;
    private int jumpCounter = 0;
    public GroundCheck groundCheck;
    public Rigidbody2D rb;


    void Update()
    {
       
       //Debug.Log(rb.velocity);
    }

    void OnMove(InputValue movePosition)
    {
        //Get Player Position
        movement = movePosition.Get<Vector2>();
 
    }

    void OnJump()
    {
        // Trigger the Jump Animation
        if (groundCheck.isGrounded == true)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            jumpCounter = 0;
            //animator.SetTrigger("Jump");
        }

        if (groundCheck.isGrounded != true && jumpCounter < 2)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            jumpCounter += 1;
           // animator.SetTrigger("Jump");
        }
    }

    private void FixedUpdate()
    {

        //Move the planet
        transform.Rotate(0, 0,  movement.x * rotationSpeed * Time.deltaTime);
        rb.velocity = new Vector2((movement.x * playerSpeed), rb.velocity.y);

    }
}
