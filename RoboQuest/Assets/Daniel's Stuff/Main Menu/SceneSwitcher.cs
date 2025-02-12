using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
