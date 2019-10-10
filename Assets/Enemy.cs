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

    public Transform GunBarrel;
    public float Damage;
    public float ShootInterval;
    private float ShootTime;

    private float Speed;
    private bool IsRunning;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private Transform player;
    private bool SeenPlayer;
    private float stoptime;
    private Animator Anim;
    private Player PlayerScript;

    public Transform Torso;

    void Start()
    {
        Anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("EnemyLookat").GetComponent<Transform>();
        PlayerScript = player.transform.parent.GetComponent<Player>();
        ShootTime = ShootInterval;
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
        if (SeenPlayer)
        {
            Quaternion lookRotation = Quaternion.LookRotation (new Vector3(player.position.x, player.position.y + 0.8f, player.position.z) - Torso.position);
            Torso.rotation = lookRotation;
        }
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
        ShootTime -= Time.deltaTime;
        agent.destination = player.position;
        agent.stoppingDistance = 3;
        IsRunning = true;
        Anim.SetBool("Running", true);
        ShootGun();

        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

        if (agent.remainingDistance < 3.1f)
            Anim.SetBool("Shooting", true);
        else
            Anim.SetBool("Shooting", false);
    }

    void NotChasing()
    {
        agent.stoppingDistance = 0;
        IsRunning = false;
        Anim.SetBool("Running", false);
        Anim.SetBool("Shooting", false);
    }

    void ShootGun()
    {
        RaycastHit hit;
        Ray ray = new Ray(GunBarrel.transform.position, -GunBarrel.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                if (ShootTime < 0.1f)
                {
                    ShootTime = ShootInterval;
                    PlayerScript.Health -= Damage;
                }
            }
        }
    }
}
