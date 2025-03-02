using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName;

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Exiting the game");
    }
}
