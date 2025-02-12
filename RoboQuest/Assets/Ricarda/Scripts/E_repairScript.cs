using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class E_repairScript : GeneralInteraction
{
    public alertManager alerScript;

    public bool hasSpareParts = false;

    public override void Interact()
    {

        if (hasSpareParts)
        {
            SceneManager.LoadSceneAsync("Engine Puzzle");
        }
        else
        {
            //Debug.Log("Spare Parts Needed!");
            alerScript.sparePartsNeeded_Alert = true;
        }
    }
}

