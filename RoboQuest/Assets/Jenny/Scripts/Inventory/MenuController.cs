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

    public void OpenCloseMenu()
    {
        if (!inventoryMenu.activeSelf)
        { inventoryMenu.SetActive(true); }
        else { inventoryMenu.SetActive(false); }
    }

}
