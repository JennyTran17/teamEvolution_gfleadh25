using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GroundCheck : MonoBehaviour
{

    CircleCollider2D circleCollider2D;
    public ContactFilter2D cast;
    public float groundDistance = -1.5F;
    RaycastHit2D[] raycastHit2D = new RaycastHit2D[5];
    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Determine if we're colliding with anything 
        int collisions = circleCollider2D.Cast(Vector2.down, cast, raycastHit2D, groundDistance);
        
        // If we collide with the ground
        if (collisions > 0)
        {
            isGrounded = true;
          //  Debug.Log("on ground");
        }
        // if we're not colliding with the ground (jumping)
        else 
        {
            isGrounded = false;
            
        }
    }
}
