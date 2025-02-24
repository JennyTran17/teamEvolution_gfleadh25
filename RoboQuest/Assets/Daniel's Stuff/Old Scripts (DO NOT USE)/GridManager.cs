using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject dotPrefab; // Assign the Dot prefab
    public int gridSize = 9; // 9x9 grid

    [System.Serializable]
    public class DotData
    {
        public Vector2Int position; // Position on the grid
        public Color color;         // Color of the dot
    }

    public DotData[] levelDots; // Define dots for the current level

    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        // Clear existing dots
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Create dots only at predefined positions
        foreach (DotData dotData in levelDots)
        {
            Vector3 dotPosition = new Vector3(dotData.position.x, dotData.position.y, 0);
            GameObject newDot = Instantiate(dotPrefab, dotPosition, Quaternion.identity, transform);
            Dot dot = newDot.GetComponent<Dot>();
            dot.DotColor = dotData.color; // Assign the dot's color
        }
    }
}
