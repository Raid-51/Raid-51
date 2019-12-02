using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Alien : MonoBehaviour
{
    public Animator Anim;
    public Transform RunPos; //Staðsetningin sem geimveran á að hlaupa að

    public bool Freed; //Er geimveran búin að vera frelsuð?

    private GameObject GameManager;
    private SwitchSceneManager SSM;
    private Objectives ObjectiveScript;

    private NavMeshAgent agent;

    void Start() //Er kyrr
    {
        GameManager = GameObject.FindGameObjectWithTag("GameController");
        SSM = GameManager.GetComponent<SwitchSceneManager>();
        SSM.AlienInLastScene = true;
        ObjectiveScript = GameManager.GetComponentInChildren<Objectives>();

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
        SSM.AlienInLastScene = false;
    }
}
