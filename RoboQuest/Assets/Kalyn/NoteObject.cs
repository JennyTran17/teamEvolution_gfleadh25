using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public GameObject blueArrow;
    public GameObject redArrow;
    public GameObject yellowArrow;
    public GameObject greenArrow;

    private bool canSpawn = false; // Prevent spawning before music starts

    public void StartSpawning()
    {
        canSpawn = true;
    }

    public void CreateArrow()
    {
        if (!canSpawn) return;

        Vector3 spawnPosition = new Vector3(Random.Range(-3, 3), Random.Range(12, 15), 0);
        int randomNumber = Random.Range(0, 4);

        GameObject newArrow = null;

        switch (randomNumber)
        {
            case 0:
                {
                    newArrow = Instantiate(blueArrow, spawnPosition, Quaternion.identity); 
                    newArrow.GetComponent<FallingScript>().sparkleEffect = GameRhythm.instance.sparkleEffect; // Assign sparkle effect
                    break;
                }
            case 1: 
                { 
                    newArrow = Instantiate(redArrow, spawnPosition, Quaternion.identity); 
                    newArrow.GetComponent<FallingScript>().sparkleEffect = GameRhythm.instance.sparkleEffect;
                    break; 
                }

            case 2:
                {
                    newArrow = Instantiate(yellowArrow, spawnPosition, Quaternion.identity); 
                    newArrow.GetComponent<FallingScript>().sparkleEffect = GameRhythm.instance.sparkleEffect; 
                    break;
                }

            case 3:
                {
                    newArrow = Instantiate(greenArrow, spawnPosition, Quaternion.identity);
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
