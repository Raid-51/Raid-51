using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public Slider Health;
    public Slider Stamina;

    public Text InteractText;
    public string[] InteractMessages; //Geymir mismunandi texta

    //Sýnir texta ef playerinn interact-ar við eitthvað
    public void Interacting(int msg)
    {
        InteractText.gameObject.SetActive(true); //Sýnir textann
        InteractText.text = InteractMessages[msg]; //Breytir textanum
    }
    //Felur textann ef playerinn er ekki að interact-a við eitthvað
    public void NotInteracting()
    {
        InteractText.gameObject.SetActive(false);
    }
}
