
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public List<InventorySaveData> inventorySaveData;
    public List<int> collectedObjID = new List<int>();
  //  public List<DropItemData> droppedItems = new List<DropItemData>();
}

//[System.Serializable]
//public class DropItemData
//{
//    public int itemID;
//    public Vector3 position;

//    public DropItemData(int id, Vector3 pos)
//    {
//        itemID = id;
//        position = pos;
//    }
//}