using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNPC : GeneralInteraction
{
    private Dialogue dialogue;
    public GameObject fuelPrefab;
    private bool hasNextDialogueTriggered = false; // Track if the coin dialogue has been triggered
    private PlayerInventory playerInventory; // Reference to the player's inventory

    void Start()
    {
        dialogue = gameObject.GetComponent<Dialogue>();
        playerInventory = _playerTransform.GetComponent<PlayerInventory>(); // Get the PlayerInventory component from the player
    }

    public override void Interact()
    {
        Debug.Log("Interact function called");

        // Check if the player has the battery and if the battery dialogue hasn't been triggered yet
        if (GameManager.Instance.saveData.hasBattery && !hasNextDialogueTriggered)
        {
            // Trigger the second dialogue sequence
            dialogue.sentences = dialogue.nextSentences; // Switch to the battery dialogue sentences
            dialogue.index = 0; // Reset the dialogue index
            dialogue.textDisplay.text = ""; // Clear the text display
            dialogue.dialogueBox.SetActive(true); // Activate the dialogue box
            dialogue.StartCoroutine(dialogue.Type()); // Start typing the new dialogue
            hasNextDialogueTriggered = true; // Mark the battery dialogue as triggered
            Instantiate(fuelPrefab, transform.position, Quaternion.identity);
        }
        else if (!dialogue.dialogueBox.activeInHierarchy)
        {
            // Start the initial dialogue if the dialogue box is not active
            dialogue.dialogueBox.SetActive(true);
            Debug.Log("Open dialogue");
            
        }
    }

    
}