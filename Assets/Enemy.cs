using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float Health = 100;

    public float WalkingSpeed;
    public float RunningSpeed;

    public Transform[] Waypoints; //Geymir staðsetningarnar sem hann á að labba á milli

    public float TimeTilUninterested; //Tími til að hann hefur engann áhuga að elta spilarann

    [Header("Sight")]
    public float SightRange = 5; //Lengd sjónar hans
    public Transform SightPoint;

    public Transform GunBarrel; //Byssuholan
    public float Damage;
    public float ShootInterval; //Túmi á milli skota
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

    public Transform Torso; //Magin á honum sem snýst til að horfa á spilarann

    void Start()
    {
        Anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("EnemyLookat").GetComponent<Transform>();
        PlayerScript = player.transform.parent.GetComponent<Player>();
        ShootTime = ShootInterval;
        GotoNextPoint(); //Ganga að næstu staðsetningu
    }

    void Update()
    {
        //Ef hann er kominn nógu nálægt staðsetningunni þá fer hann að næsta
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    
        if (IsRunning) //Ef hann er hlaupandi þá aukast hraði hans
            Speed = RunningSpeed;
        else //Annars labbar hann á eðlilegum hraða
            Speed = WalkingSpeed;
        agent.speed = Speed;
        
        //Skýtur raycast til að sjá fyrir framan sig
        RaycastHit hit;
        Ray ray = new Ray(SightPoint.transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * SightRange, Color.blue);

        if (Physics.Raycast(ray, out hit, SightRange))
        {   
            //Ef það hittir spilarann , þá byrjar hann að elta hann
            if (hit.collider.tag == "Player")
            {
                SeenPlayer = true;
                stoptime = TimeTilUninterested;
            }
        }
        //Ef hann sá spilarann
        if (SeenPlayer)
        {
            if (stoptime > 0) //Ef hann er ekki enþá komið með leið á spilaranum þá heldur hann áfram að elta hann
            {
                ChasePlayer();
                stoptime -= Time.deltaTime;
            }
            else //Ef hann er kominn með leið af spilaranum þá heldur hann áfram að labba á milli punktana sína
            {
                GotoNextPoint();
                NotChasing();
                SeenPlayer = false;
            }
        }
    }

    void LateUpdate ()
    {
        //Ef hann sá spilarann þá snýr hann maganum sínum til að horfa á hann
        if (SeenPlayer)
        {
            Quaternion lookRotation = Quaternion.LookRotation (new Vector3(player.position.x, player.position.y + 0.8f, player.position.z) - Torso.position);
            Torso.rotation = lookRotation;
        }
    }

    void GotoNextPoint()
    {
        //Ef hann er kominn að punkt þá skiptir hann yfir í næsta punkt
        if (Waypoints.Length == 0)
            return;
        agent.destination = Waypoints[destPoint].position;
        destPoint = (destPoint + 1) % Waypoints.Length;
    }
    //Eltir spilarann
    void ChasePlayer()
    {
        ShootTime -= Time.deltaTime;
        agent.destination = player.position; //Eltir spilarann
        agent.stoppingDistance = 3;
        IsRunning = true;
        Anim.SetBool("Running", true);
        ShootGun(); //Skýtur spilarann

        //Snýr honum öllum til að snúa í átt að spilaranim
        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

        //Ef hann er kominn nógu nálægt þá byrjar hann að skjóta
        if (agent.remainingDistance < 3.1f)
            Anim.SetBool("Shooting", true);
        else //Annars ekki
            Anim.SetBool("Shooting", false);
    }
    //Hættir að elta spilarann
    void NotChasing()
    {
        agent.stoppingDistance = 0;
        IsRunning = false;
        Anim.SetBool("Running", false);
        Anim.SetBool("Shooting", false);
    }
    //Skýtur byssuni sinni
    void ShootGun()
    {
        //Skýtur raycast
        RaycastHit hit;
        Ray ray = new Ray(GunBarrel.transform.position, -GunBarrel.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Player") //Ef raycast-ið hittir spilarann
            {
                if (ShootTime < 0.1f) //Og hann getur skotið þá skýtur hann spilarann og hann missir líf
                {
                    ShootTime = ShootInterval;
                    PlayerScript.Health -= Damage;
                }
            }
        }
    }
}
