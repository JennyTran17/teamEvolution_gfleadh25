using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    Light2D globalLight;
    public bool isLightOn;
    float maxFlicker = 5f;  
    bool flickeringStarted = false;

    public string nextSceneName;

    private void Start()
    {
        globalLight = GameObject.Find("Global Light").GetComponent<Light2D>();
        isLightOn = false;
    }

    private void Update()
    {
        if (GameManager.Instance.saveData.completeCP && GameManager.Instance.saveData.completeFuel)
        {
            if (!flickeringStarted)
            {
                flickeringStarted = true;
                StartCoroutine(FlickerLights());
            }
        }
    }

    IEnumerator FlickerLights()
    {
        // Immediate first flicker
        ToggleLight();

        
        for (int i = 0; i < maxFlicker - 1; i++)
        {
            yield return new WaitForSeconds(1f);  // 1 second between flickers
            ToggleLight();
        }

        // Final state, lights on
        globalLight.intensity = 1f;

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextSceneName);
    }

    void ToggleLight()
    {
        if (isLightOn)
        {
            globalLight.intensity = 0.1f;
            isLightOn = false;
        }
        else
        {
            globalLight.intensity = 0.3f;
            isLightOn = true;
        }
    }
}
