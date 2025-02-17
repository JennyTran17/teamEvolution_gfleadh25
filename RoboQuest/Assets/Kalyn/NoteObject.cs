using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public GameObject crystal1;
    public GameObject crystal2;
    public GameObject spike;
  

    private bool canSpawn = false; // Prevent spawning before music starts

    public void StartSpawning()
    {
        canSpawn = true;
    }

    public void CreateArrow()
    {
        if (!canSpawn) return;

        Vector3 spawnPosition = new Vector3(Random.Range(-3, 3), Random.Range(12, 15), 0);
        int randomNumber = Random.Range(0, 3);

        GameObject newArrow = null;

        switch (randomNumber)
        {
            case 0:
                {
                    newArrow = Instantiate(crystal1, spawnPosition, Quaternion.identity); 
                    newArrow.GetComponent<FallingScript>().sparkleEffect = GameRhythm.instance.sparkleEffect; // Assign sparkle effect
                    break;
                }
            case 1: 
                { 
                    newArrow = Instantiate(crystal2, spawnPosition, Quaternion.identity); 
                    newArrow.GetComponent<FallingScript>().sparkleEffect = GameRhythm.instance.sparkleEffect;
                    break; 
                }

            case 2:
                {
                    newArrow = Instantiate(spike, spawnPosition, Quaternion.identity); 
                    newArrow.GetComponent<FallingScript>().sparkleEffect = GameRhythm.instance.sparkleEffect; 
                    break;
                }

        }

        if (newArrow != null)
        {
            FallingScript fallingScript = newArrow.GetComponent<FallingScript>();//get the falling script and attach to arrow
            if (fallingScript != null)
            {
                fallingScript.StartFalling(); // Start falling immediately
            }
        }
    }
}
