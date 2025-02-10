using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CP_repairScript : GeneralInteraction
{

    public bool hasWires = false;

    public override void Interact()
    {

        if (hasWires)
        {
            SceneManager.LoadSceneAsync("Control Panel Puzzle");
        }
        else
        {
            Debug.Log("Wires Needed!");
        }
    }
}
