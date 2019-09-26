using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Slot 1")]
    public int itemID1;
    public Image ItemIcon1;
    public Toggle ItemToggle1;

    [Header("Slot 2")]
    public int itemID2;
    public Image ItemIcon2;
    public Toggle ItemToggle2;

    [Header("Slot 3")]
    public int itemID3;
    public Image ItemIcon3;
    public Toggle ItemToggle3;

    private ItemDatabase ItemD;
    private Transform DropPoint;

    private int Scroll = 0;

    public bool Fullinventory;

    void Start()
    {
        ItemD = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemDatabase>();
        DropPoint = GameObject.FindGameObjectWithTag("Drop").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Dropitem"))
            ClearSlot();

        if (Scroll == 0)
            ItemToggle1.isOn = true;
        if (Scroll == 1)
            ItemToggle2.isOn = true;
        if (Scroll == 2)
            ItemToggle3.isOn = true;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Scroll != 2) Scroll += 1;
            else Scroll = 0;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Scroll != 0) Scroll -= 1;
            else Scroll = 2;
        }
        if (itemID1 != 0 && itemID2 != 0 && itemID3 != 0)
            Fullinventory = true;
        else
            Fullinventory = false;

    }

    public void AddToInventory(int objectid)
    {
        if (itemID1 == 0)
        {
            itemID1 = objectid;
            ItemIcon1.sprite = ItemD.Items[objectid].ObjectIcon;
        }
        else if (itemID2 == 0)
        {
            itemID2 = objectid;
            ItemIcon2.sprite = ItemD.Items[objectid].ObjectIcon;
        }
        else if (itemID3 == 0)
        {
            itemID3 = objectid;
            ItemIcon3.sprite = ItemD.Items[objectid].ObjectIcon;
        }
    }

    public void ClearSlot()
    {
        if (Scroll == 0)
        {
            Instantiate(ItemD.Items[itemID1].ObjectPrefab, DropPoint.position, DropPoint.rotation);
            itemID1 = 0;
            ItemIcon1.sprite = ItemD.Items[0].ObjectIcon;
        }
        if (Scroll == 1)
        {
            Instantiate(ItemD.Items[itemID2].ObjectPrefab, DropPoint.position, DropPoint.rotation);
            itemID2 = 0;
            ItemIcon2.sprite = ItemD.Items[0].ObjectIcon;
        }
        if (Scroll == 2)
        {
            Instantiate(ItemD.Items[itemID3].ObjectPrefab, DropPoint.position, DropPoint.rotation);
            itemID3 = 0;
            ItemIcon3.sprite = ItemD.Items[0].ObjectIcon;
        }
    }
}
