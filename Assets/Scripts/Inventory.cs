using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Slot 1")]
    public string ItemName1;
    public int itemID1;

    [Header("Slot 2")]
    public string ItemName2;
    public int itemID2;

    [Header("Slot 3")]
    public string ItemName3;
    public int itemID3;


    public void AddToInventory(string objectname, int objectid)
    {
        if (ItemName1 != null)
        {
            ItemName1 = objectname;
            itemID1 = objectid;
        }
        else if (ItemName2 != null)
        {
            ItemName2 = objectname;
            itemID2 = objectid;
        }
        else if (ItemName3 != null)
        {
            ItemName3 = objectname;
            itemID3 = objectid;
        }
    }
}
