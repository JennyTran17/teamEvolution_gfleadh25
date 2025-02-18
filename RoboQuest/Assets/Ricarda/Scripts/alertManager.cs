using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class alertManager : MonoBehaviour

{
    public TMP_Text alertText;

    // these are changed in their respective repair/fuel point
    public bool wiresNeeded_Alert = false;
    public bool fuelNeeded_Alert = false;
    public bool sparePartsNeeded_Alert = false;


    // Start is called before the first frame update
    void Start()
    {
        alertText.color = Color.white;
        alertText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        // Display appropriate Alert
        if (wiresNeeded_Alert)
        {

            alertText.color = Color.red;
            alertText.text = "Wires Needed !!!";

            StartCoroutine(displayTimer(0));

        }

        if (fuelNeeded_Alert)
        {
            alertText.color = Color.red;
            alertText.text = "Fuel Needed !!!";

            StartCoroutine(displayTimer(1));

        }

        if (sparePartsNeeded_Alert)
        {
            alertText.color = Color.red;
            alertText.text = "Spare Parts Needed !!!";

            StartCoroutine(displayTimer(2));

        }


    }


    IEnumerator displayTimer(int type)
    {
        // wait 2 seconds
        yield return new WaitForSeconds(2);


        //// Check if any other alerts are true
        if (wiresNeeded_Alert || fuelNeeded_Alert || sparePartsNeeded_Alert)
        {
            // Do nothing, as there is already another alert being displayed
        }
        else
        {
            // empty alert box
            alertText.color = Color.white;
            alertText.text = " ";
        }
      


    }
}
