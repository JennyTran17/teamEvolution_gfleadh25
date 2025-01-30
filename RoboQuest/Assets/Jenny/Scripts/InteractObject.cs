using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InteractObject : GeneralInteraction
{
    private Dialogue dialogue;
    
    
    public override void Interact()
    {
        Debug.Log("interact function");
        //implement code to interact
        dialogue = gameObject.GetComponent<Dialogue>();
        dialogue.dialogueBox.SetActive(true);
        Debug.Log("open dialogue");


    }

}
