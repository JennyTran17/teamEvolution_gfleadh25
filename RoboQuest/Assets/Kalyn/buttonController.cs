using UnityEngine;
using UnityEngine.InputSystem;

public class buttonController : MonoBehaviour
{
    // Get sprites for changing the image when it's pressed
    private SpriteRenderer theSr;
    public Sprite defaultImage;
    public Sprite pressedImage;

    private bool isPlayerInTrigger = false; // Track if player is inside the trigger

    // Start is called before the first frame update
    void Start()
    {
        // Get the sprite renderer
        theSr = GetComponent<SpriteRenderer>();
        theSr.sprite = defaultImage; // Set the default image initially
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the player enters the trigger, allow interaction
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // When the player exits the trigger, disable interaction
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInTrigger)
        {
            // Check if the 'F' key is pressed and change sprite accordingly
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                theSr.sprite = pressedImage;
            }

            // If the 'F' key is released, revert back to the default image
            if (Keyboard.current.fKey.wasReleasedThisFrame)
            {
                theSr.sprite = defaultImage;
            }
        }
    }
}
