using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractScene : GeneralInteraction
{
    public override void Interact()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (gameObject.name.Equals("Station") && IsWithinInteractDistance())
        {
            Debug.Log("enter scene station");
        }
        else if(gameObject.name.Equals("Ship") && IsWithinInteractDistance())
        {
            Debug.Log("enter scene ship");
        }
        else if(gameObject.name.Equals("caveTest") && IsWithinInteractDistance())
        {
            
            manager.SaveGame(); //save before load different scene
            SceneManager.LoadScene("caveTest");
        }

    }

    
}
