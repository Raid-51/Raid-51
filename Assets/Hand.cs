using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform HandPos; //Staðsetning handar
    
    //Inventory plássin, geymir hlutina í höndinni
    [Space]
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public int Slot1ItemID = -1;
    public int Slot2ItemID = -1;
    public int Slot3ItemID = -1;

    [Space]
    public GameObject Placeholder;

    private Inventory INV;
    private ItemDatabase ID;

    void Awake()
    {
        // Resetta Item IDin
        Slot1ItemID = -1;
        Slot2ItemID = -1;
        Slot3ItemID = -1;
    }

    void Start()
    {
        INV = GameObject.FindGameObjectWithTag("Interface").GetComponentInChildren<Inventory>();
        ID = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemDatabase>();
    }

    void Update()
    {
        //Ef spilarinn skiptir um inventory slot þá skiptist hluturinn í höndinni líka
        if (INV.Scroll == 0)
            ShowItem(true, false, false);
        else if (INV.Scroll == 1)
            ShowItem(false, true, false);
        else if (INV.Scroll == 2)
            ShowItem(false, false, true);
    }

    public void RemoveItem(int slotno)
    {
        if (slotno == 1)
        {
            Destroy(Slot1);
            Slot1 = Placeholder;
            Slot1ItemID = -1;
        }
        if (slotno == 2)
        {
            Destroy(Slot2);
            Slot2 = Placeholder;
            Slot2ItemID = -1;
        }
        if (slotno == 3)
        {
            Destroy(Slot3);
            Slot3 = Placeholder;
            Slot3ItemID = -1;
        }
    }

    public void AddItem(int id, int slot) //Setur hlut í höndina ef spilarinn tekur hann upp
    {
        if (slot == 1) //Setur hlutinn í slot 1
        {
            Slot1 = Instantiate(ID.Items[id].HandPrefab, HandPos.position, Quaternion.identity);
            Slot1ItemID = id;
            ItemPlacement(Slot1);
        }
        if (slot == 2) //Setur hlutinn í slot 2
        {
            Slot2 = Instantiate(ID.Items[id].HandPrefab, HandPos.position, Quaternion.identity);
            Slot2ItemID = id;
            ItemPlacement(Slot2);
        }
        if (slot == 3) //Setur hlutinn í slot 3
        {
            Slot3 = Instantiate(ID.Items[id].HandPrefab, HandPos.position, Quaternion.identity);
            Slot3ItemID = id;
            ItemPlacement(Slot3);
        }
    }

    void ItemPlacement(GameObject slot)
    {
        slot.transform.SetParent(HandPos.transform);
        slot.transform.localPosition = Vector3.zero;
        slot.transform.localRotation = Quaternion.identity;
    }
    
    //Sýnir hlutinn ú höndinni
    void ShowItem(bool s1, bool s2, bool s3)
    {
        Slot1.SetActive(s1);
        Slot2.SetActive(s2);
        Slot3.SetActive(s3);
    }
}
