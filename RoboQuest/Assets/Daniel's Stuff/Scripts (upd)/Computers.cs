using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Computers : GeneralInteraction
{
    [SerializeField] private ComputerInteract computerInteract;

    public override void Interact()
    {
        if (computerInteract.computerId == 1)
        {
            Debug.Log("Opened Level 1");
            SceneManager.LoadScene("Computer Level 1");
            
        }
        else if (computerInteract.computerId == 2)
        {
            Debug.Log("Opened Level 2");
            SceneManager.LoadScene("Computer Level 2");
        }
        else if (computerInteract.computerId == 3)
        {
            Debug.Log("Opened Level 3");
            SceneManager.LoadScene("Computer Level 3");
        }
    }
}
