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

    [Header("Scripts")]
    public Inventory InvScript;
    public Breakable_fence FenceScript;

    private Transform player;
    private Vector3 initialScale;
    private Camera Cam;
    private Objectives obctvs;


    void OnEnable() { SceneManager.sceneLoaded += CustomStart; }
    void OnDisable() { SceneManager.sceneLoaded -= CustomStart; }
    void CustomStart(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        obctvs = transform.parent.GetComponent<Objectives>();
        Cam = Camera.main;
    }
    private void Awake()
    {
        CustomStart(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (Cam == null)
        {
            CustomStart(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
        Plane plane = new Plane(Cam.transform.forward, Cam.transform.position);
        float dist = plane.GetDistanceToPoint(transform.position);
        transform.localScale = initialScale * dist * UIScale;

        transform.LookAt(2* transform.position - new Vector3(player.position.x, player.position.y + 1, player.position.z));

        if (GrabItem)
        {
            if (InvScript.itemID1 == ItemID || InvScript.itemID2 == ItemID || InvScript.itemID3 == ItemID)
                obctvs.ObjectiveFinished();
        }
        else if (GetToLocation)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= 3)
                obctvs.ObjectiveFinished();
        }
        else if (BreakFence)
        {
            if (FenceScript.opened)
            {
                obctvs.ObjectiveFinished();
            }
        }
        else if (FreeAliens)
        {
            if (obctvs.AliensRescued >= obctvs.MaxAliens)
                obctvs.ObjectiveFinished();
        }

    }
}
