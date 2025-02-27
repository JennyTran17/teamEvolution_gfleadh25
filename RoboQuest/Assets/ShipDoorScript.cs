using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDoorScript : MonoBehaviour
{
    public bool doorOpen = false;
    public bool doorClose = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen)
        {
            animator.SetTrigger("doorOpen");
            doorOpen = false;
        }

        if (doorClose)
        {
            animator.SetTrigger("doorClose");
            doorClose = false;
        }
    }

}
