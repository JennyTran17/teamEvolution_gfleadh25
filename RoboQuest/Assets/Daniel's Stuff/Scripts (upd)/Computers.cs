using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computers : GeneralInteraction
{
    //[SerializeField] private ComputerInteract computerInteract;
    private ComputerInteract computerInteract;

    void Awake()
    {
        computerInteract = GetComponent<ComputerInteract>();
        if (computerInteract == null )
        {
            Debug.LogError("ComputerInteract component not found on " + gameObject.name);
        }
    }

    public override void Interact()
    {
        if (computerInteract != null)
        {
            computerInteract.InteractWithComputer();
        }
    }
}
