using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlidingPuzzleManager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptySpace;
    private int size;
    public bool puzzleSolved = false;

    public Button reshuffleButton;
    private bool initialShuffle = true;

    
    private void CreateGamePieces(float gapThickness)
    {
        float width = 1 / (float)size;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                // make piece
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                // position piece in gameboard going from -1 to +1
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                  +1 - (2 * width * row) - width, 0);

                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}"; 

                // Set empty Space in bottom right
                if ((row == size - 1) && (col == size - 1))
                { 
                    emptySpace = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {   // map UV coordinates (0 -> 1)
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    // (0, 1), (1, 1), (0, 0), (1, 0)
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));

                    mesh.uv = uv;
                }
            }
        }
    }



    void Start()
    {
        pieces = new List<Transform>();
        size = 3;
        CreateGamePieces(0.01f);

        Shuffle();
    }



    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        if (SwapIfValid(i, -size, size))    { break; }
                        if (SwapIfValid(i, +size, size))    { break; }
                        if (SwapIfValid(i, -1, 0))          { break; }
                        if (SwapIfValid(i, +1, size - 1))   { break; }
                    }

                }

                if (CheckCompletion())
                {
                    Exit();
                }
            }

        }
    }


    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptySpace))
        {
            //Swap them in pieces list
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            //Swap in game (transform)
            (pieces[i].localPosition, pieces[i + offset].localPosition) = (pieces[i + offset].localPosition, pieces[i].localPosition);
            //Update empty space
            emptySpace = i;
            return true;
        }
        return false;
    }


    private bool CheckCompletion()
    {
        for (int i =0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }

        Debug.Log("WIN!!!");
        puzzleSolved = true;
        if (SceneManager.GetActiveScene().name.Equals("Engine Puzzle"))
        {
            if(GameManager.Instance != null)
            {
                Debug.Log("complete engine");
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Control Puzzle"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.completeCP();
                GameManager.Instance.SaveGame();
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Fuel Puzzle"))
        {
            if (GameManager.Instance != null)
            {
                Debug.Log("complete fuel");
            }
        }
        return true;
    }

    
    public void Shuffle()
    {
        int count = 0;
        int last = 0;

        while (count < (size * size * size))
        {
            int rnd = Random.Range(0, size * size);

            if (rnd == last) { continue; }
            last = emptySpace;

            if (SwapIfValid(rnd, -size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +1, size - 1))
            {
                count++;
            }

            // Avoid activating reshuffle button cool down on game start shuffle
            if (initialShuffle)
            {
                initialShuffle = false;
            }
            else
            {
                reshuffleButton.interactable = false;
                StartCoroutine(shuffleCooldown());
            }



        }
    }


    public void Exit()
    {
        SceneManager.LoadSceneAsync("Ship");

    }


    IEnumerator shuffleCooldown()
    {
        yield return new WaitForSeconds(15);
        reshuffleButton.interactable = true;
    }


}
