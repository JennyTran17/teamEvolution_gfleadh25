using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject inventoryMenu;
    private void Start()
    {
        inventoryMenu.SetActive(false);
    }

    private void Update()
    {
        if(Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (!inventoryMenu.activeSelf)
            { inventoryMenu.SetActive(true); }
            else { inventoryMenu.SetActive(false); }

        }
    }
   

}
