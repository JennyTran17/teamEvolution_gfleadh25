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
    public AudioSource walkingAudio;

    //public Daniel_GameManager danielGM;

    public GameObject wiresPrefab;
    public GameObject tileUnderWires;
    //public Transform wiresLocation;
    

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        animator = gameObject.GetComponent<Animator>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        walkingAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is moving -> set walking to true
        if (movement != Vector2.zero)
        {
            animator.SetBool("Walking", true);
            walkingAudio.enabled = true;

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
            walkingAudio.enabled = false;
        }

        if(GameManager.Instance.saveData.completeConnLvl1 && GameManager.Instance.saveData.completeConnLvl2 && GameManager.Instance.saveData.completeConnLvl3)
        {
            if(wiresPrefab != null)
            {
                wiresPrefab.SetActive(true);
            }
            tileUnderWires.SetActive(true);
        }
        else
        {
            if(wiresPrefab != null)
            {
                wiresPrefab.SetActive(false);
            }
            tileUnderWires.SetActive(false);
        }

    }

    //public void createWires()
    //{

    //    if (GameManager.Instance != null)
    //    {
    //        if (GameManager.Instance.saveData.completeConnLvl1 && GameManager.Instance.saveData.completeConnLvl2 && GameManager.Instance.saveData.completeConnLvl3)
    //        {
    //            //Instantiate an object with the wire sprite
    //            Instantiate(wiresPrefab, wiresLocation);
    //        }
    //    }
    //}

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
