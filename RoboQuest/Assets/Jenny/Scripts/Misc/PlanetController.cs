using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "Main Level" && GameManager.Instance.saveData.planetPosition != Quaternion.identity)
        {
            transform.rotation = GameManager.Instance.saveData.planetPosition;
        }
    }
}
