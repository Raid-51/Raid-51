using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    public Animator Anim;
    public Transform RunPos;

    public bool Freed;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = RunPos.position;
    }

    void Update()
    {
        if (agent.remainingDistance < 0.2f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Run()
    {
        Anim.SetTrigger("Run");
        //agent.destination = RunPos.position;
        agent.stoppingDistance = 0;
        Freed = true;
    }
}
