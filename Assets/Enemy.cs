using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float Health = 100;

    public float WalkingSpeed;
    public float RunningSpeed;

    public Transform[] Waypoints;

    [Header("Sight")]
    public float SightRange = 5;
    public Transform SightPoint;

    private float Speed;
    private bool IsRunning;
    private int destPoint = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

        if (IsRunning)
            Speed = RunningSpeed;
        else
            Speed = WalkingSpeed;

        agent.speed = Speed;

        RaycastHit hit;
        Ray ray = new Ray(SightPoint.transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * SightRange, Color.blue);

        if (Physics.Raycast(ray, out hit, SightRange))
        {
            if (hit.collider.tag == "Player")
            {

            }
        }
    }

    void GotoNextPoint()
    {
        if (Waypoints.Length == 0)
            return;

        agent.destination = Waypoints[destPoint].position;

        destPoint = (destPoint + 1) % Waypoints.Length;
    }
}
