using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PopUP : MonoBehaviour
{
    public GameObject popUp;
    public GameObject notice;
    private void Start()
    {
        popUp.SetActive(false);
        notice.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        notice.SetActive(true);
        if(other.gameObject.tag.Equals("Player") && Keyboard.current.fKey.isPressed)
        {
           popUp.SetActive(true);
        }
    }

    private void OnTriggerExit2D()
    {
        popUp.SetActive(false);
        notice.SetActive(false);
    }
}
