using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour
{
    Collider2D collider;
    private void Start()
    {
        collider = gameObject.GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    private void Update()
    {
        GameObject secretExit = GameObject.Find("Secret Exit");
        if(secretExit.activeSelf == false)
        {
            collider.isTrigger = false;
        }
        else
        {
            collider.isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene("Main Level");
        }
    }
}
