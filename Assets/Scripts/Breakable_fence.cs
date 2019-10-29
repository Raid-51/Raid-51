using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_fence : MonoBehaviour
{
    // breyta búin til til þess að taka eftir hvenær það á að brjóta hliðið
    public bool destroyed = false;

    void Update()
    {   
        // Þetta gerist af annað script breytir valueinu á destroyed í true
        if (destroyed == true)
        {
            // Skipta um girðingar módel
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);

            // Slökkva á girðingar collider svo að spilarinn geti labbað í gegn og til þess að aþð sé ekki hægt að ýta á girðinguna aftur
            transform.gameObject.GetComponent<BoxCollider>().enabled = false;

            // Gera destroyed false aftur til þess að triggera þetta ekki aftur
            destroyed = false;
        }
    }
}
