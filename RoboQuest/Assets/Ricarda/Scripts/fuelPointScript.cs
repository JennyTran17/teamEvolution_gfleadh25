using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fuelPointScript : GeneralInteraction
{
    // placeholder bool
    public bool hasFuel = false;

    public override void Interact()
    {
        if (hasFuel)
        {
            Debug.Log("Fuel Added");
        }
        else
        {
            Debug.Log("Fuel Needed!");
        }
        
    }
}
