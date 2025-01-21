using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private Vector2 movement;
    Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       
        Debug.Log(movement);
    }

    void OnMove(InputValue movePosition)
    {
        //Get Player Position
        movement = movePosition.Get<Vector2>();

        
    }

    private void FixedUpdate()
    {

        //Move the player
        transform.Rotate(0, 0,  movement.x * rotationSpeed * Time.deltaTime);


    }
}
