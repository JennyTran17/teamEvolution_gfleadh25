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
            manager.SaveGame();
            Debug.Log("enter scene station");
            SceneManager.LoadScene("Station");
        }
        else if(gameObject.name.Equals("Ship") && IsWithinInteractDistance())
        {
            manager.SaveGame();
            Debug.Log("enter scene ship");
            SceneManager.LoadScene("Ship");
        }
        else if(gameObject.name.Equals("Choir Passage") && IsWithinInteractDistance())
        {
            
            manager.SaveGame(); //save before load different scene
            SceneManager.LoadScene("Kalyn");
        }
        else if (gameObject.name.Equals("Cave") && IsWithinInteractDistance())
        {

            manager.SaveGame(); //save before load different scene
            SceneManager.LoadScene("Cave");
        }
        else if (gameObject.name.Equals("Main Level") && IsWithinInteractDistance())
        {

            manager.SaveGame(); //save before load different scene
            SceneManager.LoadScene("Main Level");
        }

    }

    
}
