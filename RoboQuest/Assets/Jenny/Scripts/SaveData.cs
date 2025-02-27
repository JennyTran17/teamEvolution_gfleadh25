
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<InventorySaveData> inventorySaveData;
    public List<int> collectedObjID = new List<int>();
    public List<DropItemController> droppedItems = new List<DropItemController>();
    public bool hasBattery= false;
    public bool hasFuel= false;
    public bool hasWire = false;
   // public bool hasSpareParts = false;
    //public bool completeEngine = false;
    public bool completeFuel = false;
    public bool completeCP = false;

    public bool completeConnLvl1 = false;
    public bool completeConnLvl2 = false;
    public bool completeConnLvl3 = false;

    public Quaternion planetPosition;
}

