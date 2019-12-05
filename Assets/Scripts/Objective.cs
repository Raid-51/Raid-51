using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour
{
    public float UIScale = 0.275f;

    [Header("What kind of objective is this?")]
    public bool GrabItem;
    public int ItemID;
    [Space]
    public bool GetToLocation;
    public bool BreakFence;
    public bool FreeAliens;

    //[Header("Scripts")]
    private Interface IF;
    private Inventory InvScript;
    private Breakable_fence FenceScript;

    private Transform player;
    private Vector3 initialScale;
    private Camera Cam;
    private Objectives obctvs;


    private void Start()
    {
        initialScale = transform.localScale; //Geymir stærð objective marker-ins til að hann stækki og minnki eðlilega
    }

    //Virkar eins og Start, nema þetta keyrir í hvert sinn sem það er skipt um scene
    void CustomStart()
    {
        Cam = Camera.main;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        obctvs = transform.parent.GetComponent<Objectives>();

        // Finna scripts
        IF = GameObject.FindGameObjectWithTag("Interface").GetComponent<Interface>();
        InvScript = IF.GetComponentInChildren<Inventory>();
        if (SceneManager.GetActiveScene().buildIndex == 0) FenceScript = GameObject.FindGameObjectWithTag("Breakable fence").GetComponent<Breakable_fence>();
    }

    void Update()
    {
        // Þetta er til þess að keyra CustomStart þegar það er búið að skipta um scene
        if (Cam == null) CustomStart();

        //Nær í staðsetningu myndavélar og stækkar marker-ið við fjarlægð
        Plane plane = new Plane(Cam.transform.forward, Cam.transform.position);
        float dist = plane.GetDistanceToPoint(transform.position);
        transform.localScale = initialScale * dist * UIScale;

        transform.LookAt(2* transform.position - new Vector3(player.position.x, player.position.y + 1, player.position.z)); //Lætur markerinn horfa á spilaran

        if (GrabItem) //Ef objective-ið er að ná í hlut, þá klárast það ef maður nær í hlutinn
        {
            if (InvScript.itemID1 == ItemID || InvScript.itemID2 == ItemID || InvScript.itemID3 == ItemID)
                obctvs.ObjectiveFinished();
        }
        else if (GetToLocation) //Ef objective-ið er að komast að staðsetningu, þá klárast það ef maður kemur að því
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= 3)
                obctvs.ObjectiveFinished();
        }
        else if (BreakFence) //Ef objective-ið er að brjóta girðinguna, þá klárast það ef maður gerir það
        {
            if (FenceScript.opened)
            {
                obctvs.ObjectiveFinished();
            }
        }
        else if (FreeAliens) //Ef objective-ið er að frelsa geimveru, þá klárast það þegar geimverunar allar eru frelsaðar
        {
            if (obctvs.AliensRescued >= obctvs.MaxAliens)
                obctvs.ObjectiveFinished();
        }

    }
}
