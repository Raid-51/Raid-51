using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_fence : MonoBehaviour
{
    public bool destroyed = false;


    void Update()
    {
        if (destroyed == true)
        {
            // Skipta um girðingar módel
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);

            // Slökkva á girðingar collider svo að spilarinn geti labbað í gegn og til þess að aþð sé ekki hægt að ýta á girðinguna aftur
            transform.gameObject.GetComponent<BoxCollider>().enabled = false;

            destroyed = false;
        }
    }
}
