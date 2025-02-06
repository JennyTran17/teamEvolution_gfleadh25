using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    public bool canbepressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame) 
        {
            if (canbepressed)
            {
                gameObject.SetActive(false);
                GameManager.instance.NoteHit();//calls method from game manager
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {//if the player collides with the buttons make them pressable
        if (other.tag == "fallingobj") 
        {
            canbepressed = true;
        
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "fallingobj")
        {
            canbepressed = false;
            GameManager.instance.NoteMissed();
        }

    }
}
