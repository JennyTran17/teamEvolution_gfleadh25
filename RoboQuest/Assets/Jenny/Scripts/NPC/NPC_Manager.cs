using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC_Manager : MonoBehaviour
{
    
    public Animator npcAnimator;
    public PlayerInventory playerInventory;
    private const float INTERACT_DISTANCE = 3.5F;

    // Start is called before the first frame update
    void Start()
    {
       npcAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(playerInventory.transform.position, transform.position) < INTERACT_DISTANCE && playerInventory.hasBattery)
        {
            npcAnimator.SetTrigger("wake");
            StartCoroutine(ChangeAnimation(3));
        }
    }

    IEnumerator ChangeAnimation(int time)
    {
        //Wait a few seconds before running the next line of code
        yield return new WaitForSeconds(time);
        npcAnimator.SetBool("newIdle", true);
        
    }
}
