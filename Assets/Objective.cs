using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public float UIScale = 0.275f;
    private Transform player;
    private Vector3 initialScale;
    private Camera Cam;

    void Start()
    {
        initialScale = transform.localScale;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Cam = Camera.main;
    }

    void Update()
    {
        Plane plane = new Plane(Cam.transform.forward, Cam.transform.position);
        float dist = plane.GetDistanceToPoint(transform.position);
        transform.localScale = initialScale * dist * UIScale;

        transform.LookAt(2* transform.position - new Vector3(player.position.x, player.position.y + 1, player.position.z));
    }
}
