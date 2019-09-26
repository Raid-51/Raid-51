using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public Slider Health;
    public Slider Stamina;

    public Text InteractText;
    public string[] InteractMessages;

    public void Interacting(int msg)
    {
        InteractText.gameObject.SetActive(true);
        InteractText.text = InteractMessages[msg];
    }

    public void NotInteracting()
    {
        InteractText.gameObject.SetActive(false);
    }
}
