using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };

    public float correctRotation;

    [SerializeField]
    bool isPlaced = false;

    FuelPuzzleManager puzzleManager;


    private void Awake()
    {
        puzzleManager = GameObject.Find("FuelPuzzleManager").GetComponent<FuelPuzzleManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        if (transform.eulerAngles.z == correctRotation)
        {
            isPlaced = true;
            puzzleManager.correctMove();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                transform.Rotate(new Vector3(0, 0, 90));

                // see if piece in right rotation or not
                if (transform.eulerAngles.z == correctRotation && isPlaced == false)
                {
                    isPlaced = true;
                    puzzleManager.correctMove();
                }
                else if (isPlaced == true)
                {
                    isPlaced = false;
                    puzzleManager.wrongMove();
                }
            }



        }
    }

}
