using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float Range;

    private Camera cam;
    private Inventory INV;
    private Interface IF;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        IF = GameObject.FindGameObjectWithTag("Interface").GetComponent<Interface>();
        INV = IF.gameObject.GetComponentInChildren<Inventory>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Range))
        {
            if (hit.collider.tag == "Object")
            {
                IF.Interacting(0);
                if (Input.GetMouseButtonDown(0))
                {
                    if (!INV.Fullinventory)
                    {
                        INV.AddToInventory(hit.collider.GetComponent<Object>().ObjectID);
                        Destroy(hit.collider.gameObject);
                    }
                }

            }
            else if (hit.collider.tag == "Breakable fence")
            {
                IF.Interacting(4);
                if (Input.GetMouseButtonDown(0))
                {
                    // Það er búið að smella á hliðið
                    //if ()
                    //{

                    //}
                }
            }
            else if (hit.collider.tag == "Door")
            {

            }
            else if (hit.collider.tag == "Elevator")
            {

            }
            else if (hit.collider.tag == "Lock")
            {

            }
            else
                IF.NotInteracting();
        }
        else
            IF.NotInteracting();
    }
}
