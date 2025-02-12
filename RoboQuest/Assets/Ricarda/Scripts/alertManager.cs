using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class alertManager : MonoBehaviour

{
    public TMP_Text alertText;

    public bool fuelNeeded_Alert = false;
    public bool wiresNeeded_Alert = false;
    public bool sparePartsNeeded_Alert = false;

    bool activeAlert = true;

    // Start is called before the first frame update
    void Start()
    {
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

            StartCoroutine(displayTimer());

            activeAlert = true;
        }

        if (fuelNeeded_Alert)
        {
            alertText.color = Color.red;
            alertText.text = "Fuel Needed !!!";

            StartCoroutine(displayTimer());

            activeAlert = true;
        }

        if (sparePartsNeeded_Alert)
        {
            alertText.color = Color.red;
            alertText.text = "Spare Parts Needed !!!";
            StartCoroutine(displayTimer());

            activeAlert = true;
        }


    }


    IEnumerator displayTimer()
    {
        
        yield return new WaitForSeconds(5);

        if (!activeAlert)
        {
            alertText.color = Color.white;
            alertText.text = " ";
        }

        wiresNeeded_Alert = false;
        fuelNeeded_Alert = false;
        sparePartsNeeded_Alert = false;
    }
}
