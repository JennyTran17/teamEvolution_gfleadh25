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
            //    if (canbepressed)
            //    {

            //        //GameManager.instance.NoteHit();//calls method from game manager
            //    }
            Debug.Log("The F key was pressed");
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {//if the object collides with the buttons make them pressable
        if (other.gameObject.tag == "bluearrow")
        {
            //canbepressed = true;
            Destroy(other.gameObject);
            Debug.Log("Delete blue arrow");

        }
        else if (other.tag == "redarrow")
        {
            canbepressed = true;

        }
        else if (other.tag == "yellowarrow")
        {
            canbepressed = true;

        }
        else if (other.tag == "greenarrow")
        {
            canbepressed = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "bluearrow")
        {
            canbepressed = false;
            GameRhythm.instance.NoteMissed();
        }
        else if (other.tag == "redarrow")
        {
            canbepressed = false;
            GameRhythm.instance.NoteMissed();
        }
        else if (other.tag == "yellowarrow")
        {
            canbepressed = false;
            GameRhythm.instance.NoteMissed();
        }
        if (other.tag == "greenarrow")
        {
            canbepressed = false;
            GameRhythm.instance.NoteMissed();

        }

    }
}

