using System.Collections.Generic;
using UnityEngine;

public class SurviveSceneSwitch : MonoBehaviour
{
    void Awake()
    {
        int withSameName = 0;
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObject.name == this.gameObject.name) withSameName++;
            if (withSameName > 1) break;
        }
        // Tékka hvort að þessi Game Controller sé ekki sá fyrsti, ef hann er ekki sá fyrsti slekkur hann á sér
        if (withSameName > 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);// Passa að objectinum sé ekki eytt
    }
}
