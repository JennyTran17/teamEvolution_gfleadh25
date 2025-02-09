using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class E_repairScript : GeneralInteraction
{

    public override void Interact()
    {
        SceneManager.LoadSceneAsync("Control Puzzle");
    }
}

