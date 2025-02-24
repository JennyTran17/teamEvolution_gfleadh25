using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Daniel_PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 1f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        animator = gameObject.GetComponent<Animator>();
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is moving -> set walking to true
        if (movement != Vector2.zero)
        {
            animator.SetBool("Walking", true);

            if (movement.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        // If the player is not moving  -> cancel the walking animation
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void FixedUpdate()
    {
        // Move the Player
        rb.MovePosition(rb.position + (movement * playerSpeed * Time.fixedDeltaTime));
    }

    void OnMove(InputValue movePosition)
    {
        movement = movePosition.Get<Vector2>();
    }
}
