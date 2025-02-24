using UnityEngine;
using TMPro;

public class ComputerInteract : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private int computerId; // Unique ID for this computer
    [SerializeField] private GameObject puzzleOverlayObject;

    private PuzzleOverlay puzzleOverlay;

    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.F;

    [Header("UI")]
    [SerializeField] private GameObject interactionPrompt; // UI element showing "Press F to interact"
    [SerializeField] private TextMeshProUGUI promptText;

    private bool playerInRange = false;
    private Transform player;

    private void Awake()
    {
        //// Hide the prompt initially
        //if (interactionPrompt != null)
        //{
        //    interactionPrompt.SetActive(false);
        //    Debug.Log("Hiding prompt on start");
        //}

        //// Set default prompt text if not set
        //if (promptText != null)
        //{
        //    promptText.text = $"Press {interactionKey} to use computer";
        //}

        if (puzzleOverlayObject != null)
        {
            puzzleOverlay = puzzleOverlayObject.GetComponent<PuzzleOverlay>();
            if (puzzleOverlay == null)
            {
                Debug.LogError("PuzzleOverlay component not found on " + puzzleOverlayObject.name);
            }
        }

        
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
            Debug.Log("Hiding prompt in Awake");
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            // Check if player is still in range (in case they moved)
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance > interactionDistance)
                {
                    PlayerExitRange();
                    return;
                }
            }

            // Check for interaction key press
            if (Input.GetKeyDown(interactionKey))
            {
                InteractWithComputer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger Enter with: {other.gameObject.name}, Has Player tag: {other.CompareTag("Player")}");
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            PlayerEnterRange();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerExitRange();
        }
    }

    private void PlayerEnterRange()
    {
        playerInRange = true;
        if (interactionPrompt != null)
            interactionPrompt.SetActive(true);
    }

    private void PlayerExitRange()
    {
        playerInRange = false;
        player = null;
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    public void InteractWithComputer()
    {
        if (puzzleOverlay != null)
        {
            // Hide the interaction prompt while puzzle is active
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);

            // Activate the puzzle overlay
            puzzleOverlay.ActivatePuzzle(computerId);
        }
        else
        {
            Debug.LogWarning("PuzzleOverlay reference not set on " + gameObject.name);
        }
    }

    // Optional: Visualize the interaction range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}