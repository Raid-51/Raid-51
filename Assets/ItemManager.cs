using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> AllPickups;

    // Þessi kóði þarf allur að keyra á undan Start methodunum
    void Awake()
    {
        // Tékka hvort að þessi Game Controller sé ekki sá fyrsti, ef hann er ekki sá fyrsti slekkur hann á sér
        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
            this.gameObject.SetActive(false);
        else
        {
            DontDestroyOnLoad(this.gameObject);// Passa að objectinum sé ekki eytt
            
            // Bæta öllum pickupable hlutonum í AllPickups listann
            foreach (GameObject Pickup in GameObject.FindGameObjectsWithTag("Object"))
            {
                AllPickups.Add(Pickup);
            }
        }
    }
}
