using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class RestartGame : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string mainMenuSceneName = "Main Menu"; 

    void Start()
    {
        // Make sure the video player triggers the scene change when video ends
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        StartCoroutine(LoadMainMenuAfterDelay());
    }

    IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(2f);  
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
