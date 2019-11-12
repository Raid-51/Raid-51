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
    public Inventory INV;
    public ItemDatabase ID;

    private Rigidbody[] rbs;
    private BoxCollider[] boxc;
    private Object[] obj;

    public bool disablescript; //Skriptin er ókláruð, svo það er disable-að á stundinni

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

    public void RemoveItem() //tekur hlut úr höndinni ef spilarinn droppar hlutinum
    {
        
    }

    public void AddItem(int id, int slot) //Setur hlut í höndina ef spilarinn tekur hann upp
    {
        if (!disablescript)
        {
            if (slot == 1) //Setur hlutinn í slot 1
            {
                Slot1 = Instantiate(ID.Items[id].ObjectPrefab, HandPos.position, Quaternion.identity);
                Slot1.transform.SetParent(HandPos.transform);
            }
            if (slot == 2) //Setur hlutinn í slot 2
            {
                Slot2 = Instantiate(ID.Items[id].ObjectPrefab, HandPos.position, Quaternion.identity);
                Slot2.transform.SetParent(HandPos.transform);
            }
            if (slot == 3) //Setur hlutinn í slot 3
            {
                Slot3 = Instantiate(ID.Items[id].ObjectPrefab, HandPos.position, Quaternion.identity);
                Slot3.transform.SetParent(HandPos.transform);
            }

            StaticItem(); //Lætur hlutinn vera fastan
        }
    }
    
    //Sýnir hlutinn ú höndinni
    void ShowItem(bool s1, bool s2, bool s3)
    {
        if (!disablescript)
        {
            if (Slot1 != null) Slot1.SetActive(s1);
            if (Slot1 != null) Slot2.SetActive(s2);
            if (Slot1 != null) Slot3.SetActive(s3);
        }
    }

    //Lætur hlutinn vera fastan með því að eyða rigifbody, boxcollider og object skriptinni
    void StaticItem()
    {
        //rbs = FindObjectOfType<Rigidbody>();
        //foreach (Rigidbody rb in transform) Destroy(rb);
        //foreach (BoxCollider bx in transform) Destroy(bx);
        //foreach (Rigidbody rib in rbs) Destroy(rib);

        foreach(Transform child in transform)
        {
            if (child.gameObject.GetComponent<Rigidbody>() != null) print("bong");
                //Destroy(child.gameObject.GetComponent<Rigidbody>());
            if (child.gameObject.GetComponent<BoxCollider>() != null) print("bong");
            //Destroy(child.gameObject.GetComponent<BoxCollider>());
            if (child.gameObject.GetComponent<Object>() != null) print("bong");
            //Destroy(child.gameObject.GetComponent<Object>());
        }
    }
}
