using UnityEngine;

[System.Serializable]
public class DropItemController
{
    public int itemID;
    public Vector3 position;

    public DropItemController(int id, Vector3 pos)
    {
        itemID = id;
        position = pos;
    }
}
