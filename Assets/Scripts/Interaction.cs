using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float Range;

    private Camera cam;
    private Inventory INV;
    private Interface IF;
    private Breakable_fence BF;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        IF = GameObject.FindGameObjectWithTag("Interface").GetComponent<Interface>();
        INV = IF.gameObject.GetComponentInChildren<Inventory>();

        // Breakable fence
        BF = GameObject.FindGameObjectWithTag("Breakable fence").GetComponent<Breakable_fence>();
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
                if (INV.CurrentItemID() == 2)
                {
                    IF.Interacting(4);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Breakable_fence fence_hit_script = hit.collider.gameObject.GetComponent<Breakable_fence>();

                        fence_hit_script.destroyed = true;
                    }
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
