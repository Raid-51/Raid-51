using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //Geymir allar upplýsingarnar fyrir slot 1 í inventory-inu
    [Header("Slot 1")]
    public int itemID1;
    public Image ItemIcon1;
    public Toggle ItemToggle1;

    //Geymir allar upplýsingarnar fyrir slot 2 í inventory-inu
    [Header("Slot 2")]
    public int itemID2;
    public Image ItemIcon2;
    public Toggle ItemToggle2;

    //Geymir allar upplýsingarnar fyrir slot 3 í inventory-inu
    [Header("Slot 3")]
    public int itemID3;
    public Image ItemIcon3;
    public Toggle ItemToggle3;

    private ItemDatabase ItemD;
    private Transform DropPoint;

    private int Scroll = 0; //Hvaða slot er highlightað

    public bool Fullinventory;

    void Start()
    {
        ItemD = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemDatabase>();
        DropPoint = GameObject.FindGameObjectWithTag("Drop").GetComponent<Transform>();
    }

    void Update()
    {   
        //Ef spilarinn ýtir á "Q" þá droppar hann hlutinum sem hann heldur á 
        if (Input.GetButtonDown("Dropitem"))
            ClearSlot();

        //Highlightar slot-ið sem er í gangi
        if (Scroll == 0)
            ItemToggle1.isOn = true;
        if (Scroll == 1)
            ItemToggle2.isOn = true;
        if (Scroll == 2)
            ItemToggle3.isOn = true;

        //Þegar spilarinn snýr hjólinu á músinni þá skiptir hann um slot, virkar í báðar áttir og loop-ar
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
        
        //Ef öll slot-inn eru full þá er inventory-ið fullt
        if (itemID1 != 0 && itemID2 != 0 && itemID3 != 0)
            Fullinventory = true;
        //Annars er það ekki
        else
            Fullinventory = false;

    }
    //Setur hlut inn í inventory-ið
    public void AddToInventory(int objectid)
    {
        //Ef slot 1 er tómt þá fer hluturinn inn í það
        if (itemID1 == 0)
        {
            itemID1 = objectid;
            ItemIcon1.sprite = ItemD.Items[objectid].ObjectIcon;
        }
        //Annars ef slot 2 er tómt þá fer hluturinn inn í það
        else if (itemID2 == 0)
        {
            itemID2 = objectid;
            ItemIcon2.sprite = ItemD.Items[objectid].ObjectIcon;
        }
        //Annars ef slot 3 er tómt þá fer hluturinn inn í það
        else if (itemID3 == 0)
        {
            itemID3 = objectid;
            ItemIcon3.sprite = ItemD.Items[objectid].ObjectIcon;
        }
    }
    //Skilar til baka hvaða hlutur er í highlight-aða slot-inu
    public int CurrentItemID()
    {
        switch (Scroll)
        {
            case 0:
                return itemID1;
            case 1:
                return itemID2;
            default:
                return itemID3;
        }
    }
    //Hreynsar inventory-ið með því að droppa hlutinum
    public void ClearSlot()
    {
        //Ef slot 1 er highlightað
        if (Scroll == 0)
        {
            Instantiate(ItemD.Items[itemID1].ObjectPrefab, DropPoint.position, DropPoint.rotation); //Setur hlutinn aftur í veröldina fyrir framan spilaran
            //Hreynsar inventory slot-ið
            itemID1 = 0;
            ItemIcon1.sprite = ItemD.Items[0].ObjectIcon;
        }
        //Ef slot 2 er highlightað
        if (Scroll == 1)
        {
            Instantiate(ItemD.Items[itemID2].ObjectPrefab, DropPoint.position, DropPoint.rotation); //Setur hlutinn aftur í veröldina fyrir framan spilaran
            //Hreynsar inventory slot-ið
            itemID2 = 0;
            ItemIcon2.sprite = ItemD.Items[0].ObjectIcon;
        }
        //Ef slot 3 er highlightað
        if (Scroll == 2)
        {
            Instantiate(ItemD.Items[itemID3].ObjectPrefab, DropPoint.position, DropPoint.rotation); //Setur hlutinn aftur í veröldina fyrir framan spilaran
            //Hreynsar inventory slot-ið
            itemID3 = 0;
            ItemIcon3.sprite = ItemD.Items[0].ObjectIcon;
        }
    }
}
