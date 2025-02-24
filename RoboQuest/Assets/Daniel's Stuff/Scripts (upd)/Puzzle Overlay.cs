using UnityEngine;
using UnityEngine.UI;

public class PuzzleOverlay : MonoBehaviour
{
    [SerializeField] private Canvas puzzleCanvas;
    [SerializeField] private Camera puzzleCamera;
    [SerializeField] private Level[] levels; // Array of different puzzle levels
    //[SerializeField] private Daniel_GameManager puzzleManager;

    private bool isActive = false;
    private int currentComputerId = 0;

    // Change this line temporarily
    public GameObject puzzleManagerObject;
    private Daniel_GameManager puzzleManager;

    private void Start()
    {
        // Get the component from the GameObject
        if (puzzleManagerObject != null)
        {
            puzzleManager = puzzleManagerObject.GetComponent<Daniel_GameManager>();
            if (puzzleManager == null)
            {
                Debug.LogError("Could not find Daniel_GameManager on the specified object!");
            }
        }

        // Ensure puzzle is hidden at start
        SetPuzzleActive(false);
    }

    //private void Start()
    //{
    //    // Initially hide the puzzle
    //    SetPuzzleActive(false);
    //}

    public void ActivatePuzzle(int computerId)
    {
        Debug.Log("ActivatePuzzle called with ID: " + computerId);
        currentComputerId = computerId;
        if (levels == null || levels.Length == 0)
        {
            Debug.LogError("No levels assigned in PuzzleOverlay");
            return;
        }

        if (puzzleManager == null)
        {
            Debug.LogError("PuzzleManager is null");
            return;
        }

        // Load the appropriate level
        puzzleManager.SetLevel(levels[computerId]);
        SetupPuzzleCamera();
        SetPuzzleActive(true);

        // Disable player movement/controls here
        // YourPlayerController.Instance.enabled = false;
    }

    public void DeactivatePuzzle()
    {
        SetPuzzleActive(false);

        // Re-enable player movement/controls here
        // YourPlayerController.Instance.enabled = true;
    }

    private void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivatePuzzle();
        }
    }

    private void SetPuzzleActive(bool active)
    {
        Debug.Log($"SetPuzzleActive called with active = {active}");
        isActive = active;

        if (puzzleCanvas != null)
        {
            puzzleCanvas.gameObject.SetActive(active);

            // Also enable/disable the puzzle manager when activating/deactivating
            if (puzzleManager != null)
            {
                puzzleManager.enabled = active;
            }
        }
        else
        {
            Debug.LogError("PuzzleCanvas is null!");
        }

        if (puzzleCamera != null)
        {
            puzzleCamera.gameObject.SetActive(active);
        }
        else
        {
            Debug.LogError("PuzzleCamera is null!");
        }
    }

    private void SetupPuzzleCamera()
    {
        if (puzzleCamera != null && puzzleManager != null && puzzleManager.CurrentLevel != null)
        {
            // Get the current level dimensions
            int rows = puzzleManager.CurrentLevel.Row;
            int cols = puzzleManager.CurrentLevel.Col;

            // Calculate the center position of the screen
            float screenCenterX = Screen.width / 2f;
            float screenCenterY = Screen.height / 2f;

            // Set the camera's viewport rect to center it
            puzzleCamera.rect = new Rect(0, 0, 1, 1);

            // Position the camera to look at the center of the puzzle
            float centerX = cols * 0.5f;
            float centerY = rows * 0.5f;
            puzzleCamera.transform.position = new Vector3(centerX, centerY, -10f);

            float maxDimension = Mathf.Max(rows, cols);
            puzzleCamera.orthographicSize = maxDimension * 0.75f; 
        }
    }
}