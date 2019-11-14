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

    [Space]
    public GameObject Placeholder;

    [Header("Scripts")]
    public Inventory INV;
    public ItemDatabase ID;

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
        }
        if (slotno == 2)
        {
            Destroy(Slot2);
            Slot2 = Placeholder;
        }
        if (slotno == 3)
        {
            Destroy(Slot3);
            Slot3 = Placeholder;
        }
    }

    public void AddItem(int id, int slot) //Setur hlut í höndina ef spilarinn tekur hann upp
    {
        if (slot == 1) //Setur hlutinn í slot 1
        {
            Slot1 = Instantiate(ID.Items[id].HandPrefab, HandPos.position, Quaternion.identity);
            ItemPlacement(Slot1);
        }
        if (slot == 2) //Setur hlutinn í slot 2
        {
            Slot2 = Instantiate(ID.Items[id].HandPrefab, HandPos.position, Quaternion.identity);
            ItemPlacement(Slot2);
        }
        if (slot == 3) //Setur hlutinn í slot 3
        {
            Slot3 = Instantiate(ID.Items[id].HandPrefab, HandPos.position, Quaternion.identity);
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
