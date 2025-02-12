using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CP_repairScript : GeneralInteraction
{

    public bool hasWires = false;
    public alertManager alerScript;

    public override void Interact()
    {

        if (hasWires)
        {
            SceneManager.LoadSceneAsync("Control Puzzle");
        }
        else
        {
            //Debug.Log("Wires Needed!");
            alerScript.wiresNeeded_Alert = true;
            
        }
    }
}
