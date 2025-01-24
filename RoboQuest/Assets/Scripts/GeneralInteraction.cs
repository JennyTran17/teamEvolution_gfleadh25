using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GeneralInteraction : MonoBehaviour, Interactable
{
    // This code below is equivalent to Keyboard.current.enterKey.wasPressedThisFrame in Update()
    
    private InputAction interactAction;

    [SerializeField] private SpriteRenderer interactSprite;

    public Transform _playerTransform;
    private const float INTERACT_DISTANCE = 3F;
    private PlayerInput playerInput;


    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        // _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnKeyboardPressed(InputAction.CallbackContext context)
    {
        
        // call Interact function
        Interact();
        Debug.Log("Enter was pressed - NPC Interaction Triggered");
    }

    void Update()
    {
        if (playerInput != null && IsWithinInteractDistance())
        {
            interactAction = playerInput.actions["Interact"];
            interactAction.performed += OnKeyboardPressed;
        }
        else
        {
           // Debug.Log("No PlayerInput found in the scene!");
        }

        if (interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            //turn off the sprite
            interactSprite.gameObject.SetActive(false);
    
        }
        else if(!interactSprite.gameObject.activeSelf && IsWithinInteractDistance())
        {
            //turn on the sprite
            interactSprite.gameObject.SetActive(true);
        }
       
    }

    public abstract void Interact();

    public bool IsWithinInteractDistance()
    {
        if (Vector2.Distance(_playerTransform.position, transform.position) < INTERACT_DISTANCE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
