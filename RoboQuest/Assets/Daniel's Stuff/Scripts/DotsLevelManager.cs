using UnityEngine;

public class DotsLevelManager : MonoBehaviour
{
    public GridManager gridManager; 

    void Start()
    {
        LoadLevel(1); // Load level 1 on start
    }

    public void LoadLevel(int level)
    {
        switch (level)
        {
            case 1:
                gridManager.levelDots = new GridManager.DotData[]
                {
                    new GridManager.DotData { position = new Vector2Int(1, 1), color = Color.red },
                    new GridManager.DotData { position = new Vector2Int(7, 1), color = Color.red },
                    new GridManager.DotData { position = new Vector2Int(3, 3), color = Color.blue },
                    new GridManager.DotData { position = new Vector2Int(5, 5), color = Color.blue },
                    new GridManager.DotData { position = new Vector2Int(2, 6), color = Color.green },
                    new GridManager.DotData { position = new Vector2Int(6, 6), color = Color.green }
                };
                break;

                // Add other levels here if needed
        }

        gridManager.GenerateGrid(); // Regenerate grid with the new level data
    }
}
