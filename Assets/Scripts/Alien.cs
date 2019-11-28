using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    public Animator Anim;
    public Transform RunPos; //Staðsetningin sem geimveran á að hlaupa að

    public bool Freed; //Er geimveran búin að vera frelsuð?

    private NavMeshAgent agent;
    private Objectives ObjectiveScript;

    void Start() //Er kyrr
    {
        ObjectiveScript = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Objectives>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = RunPos.position;
        agent.isStopped = true;
    }

    void Update()
    {
        if (agent.remainingDistance < 0.2f) //Ef geimveran er komin að staðsetningunni þá eyðist hún
        {
            Destroy(this.gameObject);
        }
    }

    public void Run() //Hleypur að stað að staðsetningunni
    {
        ObjectiveScript.AliensRescued += 1;
        agent.isStopped = false;
        Anim.SetTrigger("Run");
        agent.stoppingDistance = 0;
        Freed = true;
    }
}
