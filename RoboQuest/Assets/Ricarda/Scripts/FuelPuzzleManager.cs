using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuelPuzzleManager : MonoBehaviour
{
    public GameObject PipeHolder;
    public GameObject[] Pipes;

    public int totalPipes = 0;

    [SerializeField]
    int correctedPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipeHolder.transform.childCount;

        Pipes =  new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipeHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void correctMove()
    {
        correctedPipes += 1;
        Debug.Log("correct Move");

        if (correctedPipes == totalPipes)
        {
            Debug.Log("You Win!");
            // Show You Win Screen, disable interaction, countdown and spawn fuel
        }
    }

    public void wrongMove()
    {
        correctedPipes -= 1;
        Debug.Log("wrong Move");

    }

    public void Exit()
    {
        SceneManager.LoadSceneAsync("Ship");

    }

}
