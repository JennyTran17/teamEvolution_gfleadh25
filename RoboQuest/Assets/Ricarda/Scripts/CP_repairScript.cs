using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CP_repairScript : GeneralInteraction
{

    public bool hasWires = false;
    public alertManager alertScript;

    //Override GeneralInteraction Update() to check continuously since Interact only called when press F
    private void Update()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (IsWithinInteractDistance())
        {
            if (!interactSprite.gameObject.activeSelf)
            {
                interactSprite.gameObject.SetActive(true);
            }

            if (/*!hasWires*/ !GameManager.Instance.saveData.hasWire)
            {
              alertScript.wiresNeeded_Alert = true;
            }
           
        }
        else
        {
            alertScript.wiresNeeded_Alert = false;
            if (interactSprite.gameObject.activeSelf)
            {
                interactSprite.gameObject.SetActive(false);
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

        GameManager manager = FindObjectOfType<GameManager>();
        if (gameObject.name.Equals("Control Panel Repair Point") && IsWithinInteractDistance())
        {
            if (/*hasWires*/ GameManager.Instance.saveData.hasWire)
            {
                manager.SaveGame();//save game before load different scene
                SceneManager.LoadSceneAsync("Control Puzzle");//code need to change if load scene additive to load scene on top of scene or call scene load scene active and set others inactive
                Debug.Log("enter cp repair point");
            }
            
        }
    }
}
