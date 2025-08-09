using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Daniel_GameManager : MonoBehaviour
{
    public static Daniel_GameManager Instance;

    [SerializeField] private Level _level;
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Transform _edgePrefab;
    
    // Audio Sources when interacting with the puzzle
    public AudioSource addSFX;
    public AudioSource removeSFX;
    public AudioSource solvedSFX;

    public Scene scene;
    private bool hasGameFinished;
    private Cell[,] cells;
    private List<Vector2Int> filledPoints;
    private List<Transform> edges;
    private Vector2Int startPos, endPos;
    private List<Vector2Int> directions = new List<Vector2Int>()
    {
        Vector2Int.up, Vector2Int.down,Vector2Int.left,Vector2Int.right
    };

    private void Awake()
    {
        Instance = this;
        hasGameFinished = false;
        filledPoints = new List<Vector2Int>();
        cells = new Cell[_level.Row, _level.Col];
        edges = new List<Transform>();
        SpawnLevel();
    }

    private void SpawnLevel()
    {
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = _level.Col * 0.5f;
        camPos.y = _level.Row * 0.5f;
        Camera.main.transform.position = camPos;
        Camera.main.orthographicSize = Mathf.Max(_level.Row, _level.Col) - 1.5f;

        for (int i = 0; i < _level.Row; i++)
        {
            for (int j = 0; j < _level.Col; j++)
            {
                cells[i, j] = Instantiate(_cellPrefab);
                cells[i, j].Init(_level.Data[i * _level.Col + j]);
                cells[i, j].transform.position = new Vector3(j + 0.5f, i + 0.5f, 0);
            }
        }
    }

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        if (hasGameFinished) return;
        
        // Get the position of when the mouse was clicked
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos = new Vector2Int(Mathf.FloorToInt(mousePos.y), Mathf.FloorToInt(mousePos.x));
            endPos = startPos;
        }
        // Get the position as the mouse is clicked and held down
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPos = new Vector2Int(Mathf.FloorToInt(mousePos.y), Mathf.FloorToInt(mousePos.x));

            if (!IsNeighbour()) return;

            // Initial square that will be filled
            if (AddEmpty())
            {
                // Audio for the interaction
                addSFX.Play();

                filledPoints.Add(startPos);
                filledPoints.Add(endPos);
                cells[startPos.x, startPos.y].Add();
                cells[endPos.x, endPos.y].Add();
                Transform edge = Instantiate(_edgePrefab);
                edges.Add(edge);
                edge.transform.position = new Vector3(
                    startPos.y * 0.5f + 0.5f + endPos.y * 0.5f,
                    startPos.x * 0.5f + 0.5f + endPos.x * 0.5f,
                    0f
                    );
                bool horizontal = (endPos.y - startPos.y) < 0 || (endPos.y - startPos.y) > 0;
                edge.transform.eulerAngles = new Vector3(0, 0, horizontal ? 90f : 0);
                Debug.Log("AddEmpty method is on");
            }
            // The following squares to be filled
            else if (AddToEnd())
            {
                // Audio for the interaction
                addSFX.Play();

                filledPoints.Add(endPos);
                cells[endPos.x, endPos.y].Add();
                Transform edge = Instantiate(_edgePrefab);
                edges.Add(edge);
                edge.transform.position = new Vector3(
                    startPos.y * 0.5f + 0.5f + endPos.y * 0.5f,
                    startPos.x * 0.5f + 0.5f + endPos.x * 0.5f,
                    0f
                    );
                bool horizontal = (endPos.y - startPos.y) < 0 || (endPos.y - startPos.y) > 0;
                edge.transform.eulerAngles = new Vector3(0, 0, horizontal ? 90f : 0);
                Debug.Log("AddToEnd method is on");
            }
            else if (AddToStart())
            {
                filledPoints.Insert(0, endPos);
                cells[endPos.x, endPos.y].Add();
                Transform edge = Instantiate(_edgePrefab);
                edges.Insert(0, edge);
                edge.transform.position = new Vector3(
                    startPos.y * 0.5f + 0.5f + endPos.y * 0.5f,
                    startPos.x * 0.5f + 0.5f + endPos.x * 0.5f,
                    0f
                    );
                bool horizontal = (endPos.y - startPos.y) < 0 || (endPos.y - startPos.y) > 0;
                edge.transform.eulerAngles = new Vector3(0, 0, horizontal ? 90f : 0);
                Debug.Log("AddToStart method is on");
            }
            // Remove filled squares from the latest point 
            else if (RemoveFromEnd())
            {
                // Remove SFX played on interaction
                removeSFX.Play();

                Transform removeEdge = edges[edges.Count - 1];
                edges.RemoveAt(edges.Count - 1);
                Destroy(removeEdge.gameObject);
                filledPoints.RemoveAt(filledPoints.Count - 1);
                cells[startPos.x, startPos.y].Remove();
                Debug.Log("RemoveFromEnd method is on");
            }
            else if (RemoveFromStart())
            {
                Transform removeEdge = edges[0];
                edges.RemoveAt(0);
                Destroy(removeEdge.gameObject);
                filledPoints.RemoveAt(0);
                cells[startPos.x, startPos.y].Remove();
                Debug.Log("RemoveFromStart method is on");
            }

            RemoveEmpty();
            CheckWin();
            startPos = endPos;
        }
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

    // If all the squares have been filled 
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

        // When puzzle solved, play the SFX
        solvedSFX.PlayDelayed(0.3f); // Play the sound 0.3 seconds after

        if(scene.name.Equals("Computer Level 1"))
        {
            GameManager.Instance.completeConnLevel1();
        }
        else if (scene.name.Equals("Computer Level 2"))
        {
            GameManager.Instance.completeConnLevel2();
        }
        else if (scene.name.Equals("Computer Level 3"))
        {
            GameManager.Instance.completeConnLevel3();
        }

        StartCoroutine(GameFinished());


    }

    private IEnumerator GameFinished()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Station");  
    }
}
