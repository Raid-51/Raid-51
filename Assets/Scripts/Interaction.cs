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
        INV = GetComponent<Inventory>();
        IF = GameObject.FindGameObjectWithTag("Interface").GetComponent<Interface>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Range))
        {
            if (hit.collider.tag == "Object")
                if (Input.GetMouseButtonDown(0))
                {
                    INV.AddToInventory(hit.collider.GetComponent<Object>().ObjectName, hit.collider.GetComponent<Object>().ObjectID);
                    Destroy(hit.collider.gameObject);
                }
        }
    }
}
