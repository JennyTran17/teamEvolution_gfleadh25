using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gameOverAudio : MonoBehaviour
{
    public AudioSource _gameOverAudio;
   
    private void Update()
    {
        if(GameManager.Instance != null)
        {
            if(GameManager.Instance.saveData.completeFuel && GameManager.Instance.saveData.completeCP)
            {
              
                StartCoroutine(waitTime(6));
            }
        }
    }

    IEnumerator waitTime(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        _gameOverAudio.Stop();
       
    }

}
