
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<InventorySaveData> inventorySaveData;
    public List<int> collectedObjID = new List<int>();
    public List<DropItemController> droppedItems = new List<DropItemController>();
    public bool hasBattery;
    public bool hasFuel;
    public bool hasWire;
    public bool hasSpareParts;
}

