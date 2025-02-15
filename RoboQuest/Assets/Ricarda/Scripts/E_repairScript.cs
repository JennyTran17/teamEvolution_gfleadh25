using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class E_repairScript : GeneralInteraction
{
    public alertManager alertScript;

    public bool hasSpareParts = false;

    //Override GeneralInteraction Update() to check continuously since Interact only called when press F
    private void Update()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (IsWithinInteractDistance())
        {
            if (!hasSpareParts)
            {
                alertScript.sparePartsNeeded_Alert = true;
            }
        }
        

        if (Keyboard.current.fKey.isPressed && IsWithinInteractDistance())
        {
            Interact();
            Debug.Log("interact called");
        }
    }
    public override void Interact()
    {

        //GameManager manager = FindObjectOfType<GameManager>();
        //if (gameObject.name.Equals("Engine Repair Point") && IsWithinInteractDistance())
        //{
        //    if (hasSpareParts)
        //    {
        //        manager.SaveGame();//save game before load different scene
        //        SceneManager.LoadSceneAsync("Engine Puzzle");//code need to change if load scene additive to load scene on top of scene or call scene load scene active and set others inactive
        //        Debug.Log("enter engine repair point");
        //    }
        //}
    }
}

