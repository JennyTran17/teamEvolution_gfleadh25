using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GeneralInteraction : MonoBehaviour, Interactable
{

    [SerializeField] public SpriteRenderer interactSprite;

    public Transform _playerTransform;
    
    
    private const float INTERACT_DISTANCE = 2F;
 

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && IsWithinInteractDistance())
        {
            Interact();
            Debug.Log("interact called");
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
            GameManager.Instance.SaveGame();
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
