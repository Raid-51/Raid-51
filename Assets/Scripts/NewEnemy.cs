using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NewEnemy : MonoBehaviour
{
    [Header("Enemy Values")]
    public float Health = 100;
    public float WalkingSpeed = 3;
    public float RunningSpeed = 6;
    public bool dead = false;

    [Space]
    public bool Walking = true;
    public Transform[] Waypoints; //Geymir staðsetningarnar sem hann á að labba á milli

    [Header("Sight")]
    public float SightRange = 5; //Lengd sjónar hans
    public float TimeTilUninterested = 10; //Tími til að hann hefur engann áhuga að elta spilarann
    public Transform SightPoint;
    [Space]
    public float MinStopDist = 3;
    public float MaxStopDist = 6;
    private float stopdist = 3;

    [Header("Gun")]
    public Transform GunBarrel; //Byssuholan
    public float Damage;
    public float ShootInterval; //Túmi á milli skota
    private float ShootTime;

    [Header("Torso")]
    public Transform Torso; //Magin á honum sem snýst til að horfa á spilarann
    public Transform LowerTorso; //Magin á honum sem snýst til að horfa á spilarann

    private int destPoint = 0;
    private NavMeshAgent agent;
    private Transform player;
    private bool SeenPlayer;
    private float stoptime;
    private Animator Anim;
    private Player PlayerScript;
    private Collider thisCollider;
    public GameObject VisionBlock;


    void Start()
    {
        Anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        PlayerScript = player.transform.GetComponent<Player>();
        thisCollider = GetComponent<CapsuleCollider>();
        ShootTime = ShootInterval;
        GotoNextPoint(); //Ganga að næstu staðsetningu
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f && Walking) //Ef hann er kominn nógu nálægt staðsetningunni þá fer hann að næsta
            GotoNextPoint();

        if (Vector3.Distance(SightPoint.transform.position, player.position) <= SightRange)
        {
            RaycastHit hit;
            if (Physics.Linecast(SightPoint.transform.position, new Vector3(player.position.x, player.position.y + 1f, player.position.z), out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    SeenPlayer = true;
                    stoptime = TimeTilUninterested;
                }
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

    void LateUpdate()
    {
        if (SeenPlayer) //Ef hann sá spilarann þá snýr hann maganum sínum til að horfa á hann
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(player.position.x, player.position.y + 0.5f + (player.localScale.y - 1), player.position.z) - Torso.position);
            LowerTorso.rotation = lookRotation;
            Torso.transform.localEulerAngles = new Vector3(lookRotation.x, lookRotation.y + 40.5f, 0);
        }
    }

    void GotoNextPoint() //Ef hann er kominn að punkt þá skiptir hann yfir í næsta punkt
    {
        if (Walking)
        {
            if (Waypoints.Length == 0)
                return;
            agent.destination = Waypoints[destPoint].position;
            destPoint = (destPoint + 1) % Waypoints.Length;
        }
        else
        {
            if(agent.remainingDistance > 0.5f)
                agent.destination = Waypoints[0].position;
            else
            {
                agent.isStopped = true;
            }
        }
    }

    public void ChasePlayer() //Eltir spilarann
    {
        agent.destination = player.position; //Eltir spilarann
        agent.speed = RunningSpeed;
        Anim.SetBool("Running", true);
        ShootGun(); //Skýtur spilarann

        //Snýr honum til að snúa í átt að spilaranim
        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.time * 2);

        agent.stoppingDistance = stopdist;
     
        if (agent.remainingDistance < stopdist) //Ef hann er kominn nógu nálægt þá byrjar hann að skjóta
        {
            Anim.SetBool("Shooting", true);
            stopdist = MaxStopDist;
        }
        else //Annars ekki
        {
            Anim.SetBool("Shooting", false);
            stopdist = MinStopDist;
        }
    }
    
    void NotChasing() //Hættir að elta spilarann
    {
        agent.stoppingDistance = 0;
        agent.speed = WalkingSpeed;
        Anim.SetBool("Running", false);
        Anim.SetBool("Shooting", false);
    }
  
    void ShootGun() //Skýtur byssuni sinni
    {
        ShootTime -= Time.deltaTime;
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

    public void Dead()
    {
        // Triggera death animation
        Anim.SetTrigger("Dead");

        // Stoppa vörðinn frá því að hreyfa sig eftir að deyja
        Anim.SetBool("Shooting", false);

        // Eyða öllu functionalityinu á verðinum
        Destroy(VisionBlock);
        Destroy(thisCollider);
        Destroy(agent);
        Destroy(this);
    }
}
