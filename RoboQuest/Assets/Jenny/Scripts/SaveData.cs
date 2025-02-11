
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public List<InventorySaveData> inventorySaveData;
    public List<int> collectedObjID = new List<int>();
    public List<DropItemController> droppedItems = new List<DropItemController>();
}

