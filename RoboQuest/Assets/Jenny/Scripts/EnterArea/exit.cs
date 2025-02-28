using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && SceneManager.GetActiveScene().name.Equals("Ship"))
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetTrigger("open");
        }

        else if(collision.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene("Main Level");
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && SceneManager.GetActiveScene().name.Equals("Ship"))
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetTrigger("close");
        }
    }
}
