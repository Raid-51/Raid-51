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

    public float TimeTilUninterested;

    [Header("Sight")]
    public float SightRange = 5;
    public Transform SightPoint;

    private float Speed;
    private bool IsRunning;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private Transform player;
    private bool SeenPlayer;
    private float stoptime;
    private Animator Anim;

    public Transform Torso;
    public float offset;

    void Start()
    {
        Anim = GetComponent<Animator>();
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
                player = hit.transform;
                SeenPlayer = true;
                stoptime = TimeTilUninterested;
            }
        }
        if (SeenPlayer)
        {
            if (stoptime > 0)
            {
                ChasePlayer();
                stoptime -= Time.deltaTime;
            }
            else
            {
                GotoNextPoint();
                NotChasing();
                SeenPlayer = false;
            }
        }
    }
    void LateUpdate ()
    {
        Quaternion lookRotation = Quaternion.LookRotation (player.position - -Torso.position);
        Torso.rotation *= lookRotation;
    }

    void GotoNextPoint()
    {
        if (Waypoints.Length == 0)
            return;

        agent.destination = Waypoints[destPoint].position;

        destPoint = (destPoint + 1) % Waypoints.Length;
    }

    void ChasePlayer()
    {
        agent.destination = player.position;
        agent.stoppingDistance = 3;
        IsRunning = true;

        if (agent.remainingDistance < 3.2f)
        {
            Anim.SetBool("Shooting", true);
        }
        else
            Anim.SetBool("Shooting", false);
    }

    void NotChasing()
    {
        agent.stoppingDistance = 0;
        IsRunning = false;
    }
}
