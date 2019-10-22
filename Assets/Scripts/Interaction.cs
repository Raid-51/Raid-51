﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //range til að sjá hluti
    public float Range;

    private Camera cam;
    private Inventory INV;
    private Interface IF;
    //private Breakable_fence BF;

    void Start()
    {
        //Nær í componentana
        cam = GetComponentInChildren<Camera>();
        IF = GameObject.FindGameObjectWithTag("Interface").GetComponent<Interface>();
        INV = IF.gameObject.GetComponentInChildren<Inventory>();

        // Breakable fence
        //BF = GameObject.FindGameObjectWithTag("Breakable fence").GetComponent<Breakable_fence>();
    }

    void Update()
    {
        //Skýtur raycast  í miðju skjás 
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Ef það er hlutur í raycastinu
        if (Physics.Raycast(ray, out hit, Range))
        {
            //Ef það hittir hlut sem er hægt að taka upp
            if (hit.collider.tag == "Object")
            {
                IF.Interacting(0); //Sýnir texta
                if (Input.GetMouseButtonDown(0)) //Ef spilarinn "interactar" við hlutinn (smellir á mús)
                {
                    if (!INV.Fullinventory) //Ef inventory-ið er ekki fullt
                    {
                        INV.AddToInventory(hit.collider.GetComponent<Object>().ObjectID); //Setur hlutinn í inventory-ið
                        Destroy(hit.collider.gameObject); //Eyðir hlutinum úr veröldinni
                    }
                }

            }
            //Ef það hittir girðingu sem er hægt að brjóta
            else if (hit.collider.tag == "Breakable fence")
            {
                if (INV.CurrentItemID() == 2) //Ef hluturinn er vírklippur í höndinni á spilaranum
                {
                    IF.Interacting(4); //Sýnir texta
                    if (Input.GetMouseButtonDown(0)) //Ef spilarinn "interactar" við hlutinn (smellir á mús)
                    {
                        Breakable_fence BF = hit.collider.gameObject.GetComponent<Breakable_fence>(); //Nær í fence scriptina

                        BF.destroyed = true; //Opnar girðinguna
                    }
                }
            }
            //Ef það hittir geimveru sem er hægt að frelsa
            else if (hit.collider.tag == "Alien")
            {
                if (hit.collider.GetComponent<Alien>().Freed == false)
                {
                    IF.Interacting(5); //Sýnir texta
                    if (Input.GetMouseButtonDown(0)) //Ef spilarinn "interactar" við geimeruna (smellir á mús)
                    {
                        hit.collider.GetComponent<Alien>().Run();
                    }
                }
            }
            //Ef það hittir hurð sem er hægt að opna
            else if (hit.collider.tag == "Door")
            {
                IF.Interacting(1); //Sýnir texta
                if (Input.GetMouseButtonDown(0)) //Ef spilarinn "interactar" við geimeruna (smellir á mús)
                {
                    hit.collider.GetComponent<Opendoor>().Open();
                }
            }
            //Ef það hittir liftu sem er hægt að fara í
            else if (hit.collider.tag == "Elevator")
            {

            }
            //Ef það hittir lás sem er hægt að opna
            else if (hit.collider.tag == "Lock")
            {

            }
            //Ef það hittir ekki neitt þá er spilarinn ekki að interacta við neitt
            else
                IF.NotInteracting();
        }
        //Ef það hittir ekki neitt þá er spilarinn ekki að interacta við neitt
        else
            IF.NotInteracting();
    }
}
