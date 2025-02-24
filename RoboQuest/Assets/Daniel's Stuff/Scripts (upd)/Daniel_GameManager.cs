//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Daniel_GameManager : MonoBehaviour
//{
//    public Level CurrentLevel => _level;
//    public void SetLevel(Level level)
//    {
//        //_level = level;
//        //ResetPuzzle();

//        Debug.Log("SetLevel called with level: " + (level != null ? level.name : "null"));
//        if (level == null)
//        {
//            Debug.LogError("Trying to set null level!");
//            return;
//        }
//        _level = level;
//        ResetPuzzle();
//    }

//    public void ResetPuzzle()
//    {
//        // Clear existing puzzle
//        if (cells != null)
//        {
//            foreach (var cell in cells)
//                if (cell != null)
//                    Destroy(cell.gameObject);
//        }

//        foreach (var edge in edges)
//            if (edge != null)
//                Destroy(edge.gameObject);

//        // Reset variables
//        hasGameFinished = false;
//        filledPoints.Clear();
//        edges.Clear();
//        cells = new Cell[_level.Row, _level.Col];


//        // Spawn new puzzle
//        SpawnLevel();
//    }

//    // Modify the CheckWin coroutine to use the overlay system
//    private IEnumerator GameFinished()
//    {
//        yield return new WaitForSeconds(2f);
//        // Instead of loading a new scene, deactivate the puzzle
//        GetComponent<PuzzleOverlay>().DeactivatePuzzle();
//    }



//    public static Daniel_GameManager Instance;

//    [SerializeField] private Level _level;
//    [SerializeField] private Cell _cellPrefab;
//    [SerializeField] private Transform _edgePrefab;
//    [SerializeField] private Transform puzzleElementsContainer;

//    private bool hasGameFinished;
//    private Cell[,] cells;
//    private List<Vector2Int> filledPoints;
//    private List<Transform> edges;
//    private Vector2Int startPos, endPos;
//    private List<Vector2Int> directions = new List<Vector2Int>()
//    {
//        Vector2Int.up, Vector2Int.down,Vector2Int.left,Vector2Int.right
//    };

//    private void Awake()
//    {
//        Instance = this;
//        hasGameFinished = false;
//        filledPoints = new List<Vector2Int>();
//        cells = new Cell[_level.Row, _level.Col];
//        edges = new List<Transform>();
//        SpawnLevel();
//    }

//    //private void SpawnLevel()
//    //{
//    //    //Vector3 camPos = Camera.main.transform.position;
//    //    //camPos.x = _level.Col * 0.5f;
//    //    //camPos.y = _level.Row * 0.5f;
//    //    //Camera.main.transform.position = camPos;
//    //    //Camera.main.orthographicSize = Mathf.Max(_level.Row, _level.Col) + 2f;

//    //    Debug.Log($"Spawning level with dimensions: {_level.Row}x{_level.Col}");

//    //    for (int i = 0; i < _level.Row; i++)
//    //    {
//    //        for (int j = 0; j < _level.Col; j++)
//    //        {
//    //            cells[i, j] = Instantiate(_cellPrefab);
//    //            cells[i, j].Init(_level.Data[i * _level.Col + j]);
//    //            cells[i, j].transform.position = new Vector3(j + 0.5f, i + 0.5f, 0);
//    //            Debug.Log($"Spawned cell at {i},{j}");
//    //        }
//    //    }
//    //}

//    // In Daniel_GameManager.cs
//    private void SpawnLevel()
//    {
//        //Debug.Log("Starting to spawn level");
//        //Debug.Log($"Level dimensions: {_level.Row}x{_level.Col}");

//        //for (int i = 0; i < _level.Row; i++)
//        //{
//        //    for (int j = 0; j < _level.Col; j++)
//        //    {
//        //        Vector3 position = new Vector3(j + 0.5f, i + 0.5f, 0);
//        //        Debug.Log($"Spawning cell at position: {position}");

//        //        cells[i, j] = Instantiate(_cellPrefab, position, Quaternion.identity, transform);
//        //        cells[i, j].Init(_level.Data[i * _level.Col + j]);

//        //        // Verify the cell was created
//        //        if (cells[i, j] != null)
//        //            Debug.Log($"Cell spawned successfully at {i},{j}");
//        //        else
//        //            Debug.LogError($"Failed to spawn cell at {i},{j}");
//        //    }
//        //}

//        Debug.Log($"SpawnLevel called. Level dimensions: {_level.Row}x{_level.Col}");
//        Debug.Log($"Cell prefab assigned: {_cellPrefab != null}");
//        Debug.Log($"Edge prefab assigned: {_edgePrefab != null}");

//        for (int i = 0; i < _level.Row; i++)
//        {
//            for (int j = 0; j < _level.Col; j++)
//            {
//                Vector3 spawnPosition = new Vector3(j + 0.5f, i + 0.5f, 0);
//                Debug.Log($"Attempting to spawn cell at position: {spawnPosition}");

//                cells[i, j] = Instantiate(_cellPrefab, spawnPosition, Quaternion.identity, puzzleElementsContainer);

//                if (cells[i, j] != null)
//                {
//                    cells[i, j].Init(_level.Data[i * _level.Col + j]);
//                    Debug.Log($"Successfully spawned cell at {i},{j}");
//                }
//                else
//                {
//                    Debug.LogError($"Failed to spawn cell at {i},{j}");
//                }
//            }
//        }
//    }

//    private void Update()
//    {
//        if (hasGameFinished) return;

//        if (Input.GetMouseButtonDown(0))
//        {
//            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            startPos = new Vector2Int(Mathf.FloorToInt(mousePos.y), Mathf.FloorToInt(mousePos.x));
//            endPos = startPos;
//        }
//        else if (Input.GetMouseButton(0))
//        {
//            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            endPos = new Vector2Int(Mathf.FloorToInt(mousePos.y), Mathf.FloorToInt(mousePos.x));

//            if (!IsNeighbour()) return;

//            if (AddEmpty())
//            {
//                filledPoints.Add(startPos);
//                filledPoints.Add(endPos);
//                cells[startPos.x, startPos.y].Add();
//                cells[endPos.x, endPos.y].Add();
//                Transform edge = Instantiate(_edgePrefab);
//                edges.Add(edge);
//                edge.transform.position = new Vector3(
//                    startPos.y * 0.5f + 0.5f + endPos.y * 0.5f,
//                    startPos.x * 0.5f + 0.5f + endPos.x * 0.5f,
//                    0f
//                    );
//                bool horizontal = (endPos.y - startPos.y) < 0 || (endPos.y - startPos.y) > 0;
//                edge.transform.eulerAngles = new Vector3(0, 0, horizontal ? 90f : 0);
//            }
//            else if (AddToEnd())
//            {
//                filledPoints.Add(endPos);
//                cells[endPos.x, endPos.y].Add();
//                Transform edge = Instantiate(_edgePrefab);
//                edges.Add(edge);
//                edge.transform.position = new Vector3(
//                    startPos.y * 0.5f + 0.5f + endPos.y * 0.5f,
//                    startPos.x * 0.5f + 0.5f + endPos.x * 0.5f,
//                    0f
//                    );
//                bool horizontal = (endPos.y - startPos.y) < 0 || (endPos.y - startPos.y) > 0;
//                edge.transform.eulerAngles = new Vector3(0, 0, horizontal ? 90f : 0);
//            }
//            else if (AddToStart())
//            {
//                filledPoints.Insert(0, endPos);
//                cells[endPos.x, endPos.y].Add();
//                Transform edge = Instantiate(_edgePrefab);
//                edges.Insert(0, edge);
//                edge.transform.position = new Vector3(
//                    startPos.y * 0.5f + 0.5f + endPos.y * 0.5f,
//                    startPos.x * 0.5f + 0.5f + endPos.x * 0.5f,
//                    0f
//                    );
//                bool horizontal = (endPos.y - startPos.y) < 0 || (endPos.y - startPos.y) > 0;
//                edge.transform.eulerAngles = new Vector3(0, 0, horizontal ? 90f : 0);
//            }
//            else if (RemoveFromEnd())
//            {
//                Transform removeEdge = edges[edges.Count - 1];
//                edges.RemoveAt(edges.Count - 1);
//                Destroy(removeEdge.gameObject);
//                filledPoints.RemoveAt(filledPoints.Count - 1);
//                cells[startPos.x, startPos.y].Remove();
//            }
//            else if (RemoveFromStart())
//            {
//                Transform removeEdge = edges[0];
//                edges.RemoveAt(0);
//                Destroy(removeEdge.gameObject);
//                filledPoints.RemoveAt(0);
//                cells[startPos.x, startPos.y].Remove();
//            }

//            RemoveEmpty();
//            CheckWin();
//            startPos = endPos;
//        }
//    }

//    private bool AddEmpty()
//    {
//        if (edges.Count > 0) return false;
//        if (cells[startPos.x, startPos.y].Filled) return false;
//        if (cells[endPos.x, endPos.y].Filled) return false;
//        return true;


//    }

//    private bool AddToEnd()
//    {
//        if (filledPoints.Count < 2) return false;
//        Vector2Int pos = filledPoints[filledPoints.Count - 1];
//        Cell lastCell = cells[pos.x, pos.y];
//        if (cells[startPos.x, startPos.y] != lastCell) return false;
//        if (cells[endPos.x, endPos.y].Filled) return false;
//        return true;
//    }

//    private bool AddToStart()
//    {
//        if (filledPoints.Count < 2) return false;
//        Vector2Int pos = filledPoints[0];
//        Cell lastCell = cells[pos.x, pos.y];
//        if (cells[startPos.x, startPos.y] != lastCell) return false;
//        if (cells[endPos.x, endPos.y].Filled) return false;
//        return true;
//    }

//    private bool RemoveFromEnd()
//    {
//        if (filledPoints.Count < 2) return false;
//        Vector2Int pos = filledPoints[filledPoints.Count - 1];
//        Cell lastCell = cells[pos.x, pos.y];
//        if (cells[startPos.x, startPos.y] != lastCell) return false;
//        pos = filledPoints[filledPoints.Count - 2];
//        lastCell = cells[pos.x, pos.y];
//        if (cells[endPos.x, endPos.y] != lastCell) return false;
//        return true;
//    }
//    private bool RemoveFromStart()
//    {
//        if (filledPoints.Count < 2) return false;
//        Vector2Int pos = filledPoints[0];
//        Cell lastCell = cells[pos.x, pos.y];
//        if (cells[startPos.x, startPos.y] != lastCell) return false;
//        pos = filledPoints[1];
//        lastCell = cells[pos.x, pos.y];
//        if (cells[endPos.x, endPos.y] != lastCell) return false;
//        return true;
//    }

//    private void RemoveEmpty()
//    {
//        if (filledPoints.Count != 1) return;
//        cells[filledPoints[0].x, filledPoints[0].y].Remove();
//        filledPoints.RemoveAt(0);
//    }

//    private bool IsNeighbour()
//    {
//        return IsValid(startPos) && IsValid(endPos) && directions.Contains(startPos - endPos);
//    }

//    private bool IsValid(Vector2Int pos)
//    {
//        return pos.x >= 0 && pos.y >= 0 && pos.x < _level.Row && pos.y < _level.Col;
//    }

//    private void CheckWin()
//    {
//        for (int i = 0; i < _level.Row; i++)
//        {
//            for (int j = 0; j < _level.Col; j++)
//            {
//                if (!cells[i, j].Filled)
//                    return;
//            }
//        }

//        hasGameFinished = true;
//        StartCoroutine(GameFinished());
//    }

//    //private IEnumerator GameFinished()
//    //{
//    //    yield return new WaitForSeconds(2f);
//    //    UnityEngine.SceneManagement.SceneManager.LoadScene(0);

//    //    // Level change
//    //    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
//    //}
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daniel_GameManager : MonoBehaviour
{
    #region Singleton
    public static Daniel_GameManager Instance;
    #endregion

    #region Properties
    public Level CurrentLevel => _level;
    #endregion

    #region Serialized Fields
    [Header("Level Settings")]
    [SerializeField] private Level _level;
    [SerializeField] private Transform puzzleElementsContainer;

    [Header("Prefabs")]
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Transform _edgePrefab;
    #endregion

    #region Private Fields
    private bool hasGameFinished;
    private Cell[,] cells;
    private List<Vector2Int> filledPoints;
    private List<Transform> edges;
    private Vector2Int startPos, endPos;
    private readonly List<Vector2Int> directions = new List<Vector2Int>()
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        Instance = this;
        InitializeVariables();
        // Don't spawn level here anymore
    }

    private void Update()
    {
        if (hasGameFinished) return;
        HandleInput();
    }
    #endregion

    #region Public Methods
    public void SetLevel(Level level)
    {
        Debug.Log("SetLevel called with level: " + (level != null ? level.name : "null"));
        if (level == null)
        {
            Debug.LogError("Trying to set null level!");
            return;
        }
        _level = level;
        ResetPuzzle();
    }

    public void ResetPuzzle()
    {
        ClearExistingPuzzle();
        ResetVariables();
        SpawnLevel();
    }
    #endregion

    #region Private Methods
    private void InitializeVariables()
    {
        hasGameFinished = false;
        filledPoints = new List<Vector2Int>();
        edges = new List<Transform>();
    }

    private void ClearExistingPuzzle()
    {
        if (cells != null)
        {
            foreach (var cell in cells)
                if (cell != null)
                    Destroy(cell.gameObject);
        }

        foreach (var edge in edges)
            if (edge != null)
                Destroy(edge.gameObject);
    }

    private void ResetVariables()
    {
        hasGameFinished = false;
        filledPoints.Clear();
        edges.Clear();
        cells = new Cell[_level.Row, _level.Col];
    }

    private void SpawnLevel()
    {
        Debug.Log($"SpawnLevel called. Level dimensions: {_level.Row}x{_level.Col}");
        Debug.Log($"Cell prefab assigned: {_cellPrefab != null}");
        Debug.Log($"Edge prefab assigned: {_edgePrefab != null}");

        for (int i = 0; i < _level.Row; i++)
        {
            for (int j = 0; j < _level.Col; j++)
            {
                Vector3 spawnPosition = new Vector3(j + 0.5f, i + 0.5f, 0);
                Debug.Log($"Attempting to spawn cell at position: {spawnPosition}");

                cells[i, j] = Instantiate(_cellPrefab, spawnPosition, Quaternion.identity, puzzleElementsContainer);

                if (cells[i, j] != null)
                {
                    cells[i, j].Init(_level.Data[i * _level.Col + j]);
                    Debug.Log($"Successfully spawned cell at {i},{j}");
                }
                else
                {
                    Debug.LogError($"Failed to spawn cell at {i},{j}");
                }
            }
        }
    }

    // Allows the puzzle to be interacted
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos = new Vector2Int(Mathf.FloorToInt(mousePos.y), Mathf.FloorToInt(mousePos.x));
            endPos = startPos;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPos = new Vector2Int(Mathf.FloorToInt(mousePos.y), Mathf.FloorToInt(mousePos.x));

            if (!IsNeighbour()) return;

            HandleCellConnection();
        }
    }

    private void HandleCellConnection()
    {
        if (AddEmpty())
        {
            AddConnection(true, true);
        }
        else if (AddToEnd())
        {
            AddConnection(false, true);
        }
        else if (AddToStart())
        {
            AddConnection(false, false);
        }
        else if (RemoveFromEnd())
        {
            RemoveConnection(true);
        }
        else if (RemoveFromStart())
        {
            RemoveConnection(false);
        }

        RemoveEmpty();
        CheckWin();
        startPos = endPos;
    }

    private void AddConnection(bool addBothPoints, bool addToEnd)
    {
        if (addBothPoints)
        {
            filledPoints.Add(startPos);
            filledPoints.Add(endPos);
            cells[startPos.x, startPos.y].Add();
        }

        if (addToEnd)
        {
            filledPoints.Add(endPos);
        }
        else
        {
            filledPoints.Insert(0, endPos);
        }

        cells[endPos.x, endPos.y].Add();
        CreateEdge(addToEnd);
    }

    private void CreateEdge(bool addToEnd)
    {
        Transform edge = Instantiate(_edgePrefab, puzzleElementsContainer);
        if (addToEnd)
        {
            edges.Add(edge);
        }
        else
        {
            edges.Insert(0, edge);
        }

        edge.position = new Vector3(
            startPos.y * 0.5f + 0.5f + endPos.y * 0.5f,
            startPos.x * 0.5f + 0.5f + endPos.x * 0.5f,
            0f
        );

        bool horizontal = (endPos.y - startPos.y) != 0;
        edge.eulerAngles = new Vector3(0, 0, horizontal ? 90f : 0);
    }

    private void RemoveConnection(bool fromEnd)
    {
        Transform removeEdge;
        if (fromEnd)
        {
            removeEdge = edges[edges.Count - 1];
            edges.RemoveAt(edges.Count - 1);
            filledPoints.RemoveAt(filledPoints.Count - 1);
        }
        else
        {
            removeEdge = edges[0];
            edges.RemoveAt(0);
            filledPoints.RemoveAt(0);
        }

        Destroy(removeEdge.gameObject);
        cells[startPos.x, startPos.y].Remove();
    }

    private bool AddEmpty()
    {
        if (edges.Count > 0) return false;
        if (cells[startPos.x, startPos.y].Filled) return false;
        if (cells[endPos.x, endPos.y].Filled) return false;
        return true;
    }

    private bool AddToEnd()
    {
        if (filledPoints.Count < 2) return false;
        Vector2Int pos = filledPoints[filledPoints.Count - 1];
        Cell lastCell = cells[pos.x, pos.y];
        if (cells[startPos.x, startPos.y] != lastCell) return false;
        if (cells[endPos.x, endPos.y].Filled) return false;
        return true;
    }

    private bool AddToStart()
    {
        if (filledPoints.Count < 2) return false;
        Vector2Int pos = filledPoints[0];
        Cell lastCell = cells[pos.x, pos.y];
        if (cells[startPos.x, startPos.y] != lastCell) return false;
        if (cells[endPos.x, endPos.y].Filled) return false;
        return true;
    }

    private bool RemoveFromEnd()
    {
        if (filledPoints.Count < 2) return false;
        Vector2Int pos = filledPoints[filledPoints.Count - 1];
        Cell lastCell = cells[pos.x, pos.y];
        if (cells[startPos.x, startPos.y] != lastCell) return false;
        pos = filledPoints[filledPoints.Count - 2];
        lastCell = cells[pos.x, pos.y];
        if (cells[endPos.x, endPos.y] != lastCell) return false;
        return true;
    }

    private bool RemoveFromStart()
    {
        if (filledPoints.Count < 2) return false;
        Vector2Int pos = filledPoints[0];
        Cell lastCell = cells[pos.x, pos.y];
        if (cells[startPos.x, startPos.y] != lastCell) return false;
        pos = filledPoints[1];
        lastCell = cells[pos.x, pos.y];
        if (cells[endPos.x, endPos.y] != lastCell) return false;
        return true;
    }

    private void RemoveEmpty()
    {
        if (filledPoints.Count != 1) return;
        cells[filledPoints[0].x, filledPoints[0].y].Remove();
        filledPoints.RemoveAt(0);
    }

    private bool IsNeighbour()
    {
        return IsValid(startPos) && IsValid(endPos) && directions.Contains(startPos - endPos);
    }

    private bool IsValid(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < _level.Row && pos.y < _level.Col;
    }

    private void CheckWin()
    {
        for (int i = 0; i < _level.Row; i++)
        {
            for (int j = 0; j < _level.Col; j++)
            {
                if (!cells[i, j].Filled)
                    return;
            }
        }

        hasGameFinished = true;
        StartCoroutine(GameFinished());
    }

    private IEnumerator GameFinished()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<PuzzleOverlay>().DeactivatePuzzle();
    }
    #endregion
}
