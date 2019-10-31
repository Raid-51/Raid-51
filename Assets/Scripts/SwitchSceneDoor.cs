using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchSceneDoor : MonoBehaviour
{
    public int SceneNumber;
    public int InteractionTextID;
    public bool Locked = true;

    private GameObject IFGameObject;

    void Start()
    {
        IFGameObject = GameObject.FindGameObjectWithTag("Interface");
    }

    public void SwitchScene()
    {
        Collider collider = this.GetComponent<BoxCollider>();
        collider.enabled = false;

        DontDestroyOnLoad(IFGameObject);

        foreach (GameObject Pickupable in GameObject.FindGameObjectsWithTag("Object"))
        {
            DontDestroyOnLoad(Pickupable);
        }

        SceneManager.LoadScene(SceneNumber);
    }
}