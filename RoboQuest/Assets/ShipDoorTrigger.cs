using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDoorTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject door;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals(player.name))
        {
            Debug.Log("Enter: " + collision.name);
            animator.SetTrigger("doorOpen");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals(player.name))
        {
            Debug.Log("Exit: " + collision.name);
            animator.SetTrigger("doorClose");
        }
    }
}
