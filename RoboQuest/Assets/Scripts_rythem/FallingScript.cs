using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FallingScript : MonoBehaviour
{
    public float beatTempo;//speed of fall
    public bool hasStarted;
    // Start is called before the first frame update
    void Start()
    {// set the speed of the falling objects
        beatTempo = beatTempo / 60f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            Debug.Log("Press space key to start");
            //press space key to start game
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                hasStarted = true;
            }
        }
        else
        {
            // set the objects to fall on the y axis (-= or will fall to middle)
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);

        }
    }
}
