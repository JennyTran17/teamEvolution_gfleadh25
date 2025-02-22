using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class fuelPointScript : GeneralInteraction
{
    public alertManager alertScript;
    
    // placeholder bool
    public bool hasFuel = false;
    public bool fuelNeeded_Alert = true;

    //Override GeneralInteraction Update() to check continuously since Interact only called when press F
    private void Update()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (IsWithinInteractDistance())
        {
            if (/*!hasFuel*/ !GameManager.Instance.saveData.hasFuel)
            {
                alertScript.fuelNeeded_Alert = true;
            }
        }
        else
        {
            alertScript.fuelNeeded_Alert = false;
        }


        if (Keyboard.current.fKey.isPressed && IsWithinInteractDistance())
        {
            Interact();
            Debug.Log("interact called");
        }
    }

    public override void Interact()
    {

        GameManager manager = FindObjectOfType<GameManager>();
        if (gameObject.name.Equals("Fuel Point") && IsWithinInteractDistance())
        {
            if (/*hasFuel*/ GameManager.Instance.saveData.hasFuel)
            {
                manager.SaveGame(); //save game before load different scene
                SceneManager.LoadSceneAsync("Fuel Puzzle"); //code need to change if load scene additive to load scene on top of scene or call scene load scene active and set others inactive
                Debug.Log("enter fuel point");
            }
        }

    }
}
