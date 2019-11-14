using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    public int CurrentObjective;

    [Space]
    public GameObject[] AllObjectives;


    // Start is called before the first frame update
    void Start()
    {
        AllObjectives[CurrentObjective].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectiveFinished()
    {
        AllObjectives[CurrentObjective].SetActive(false);
        CurrentObjective += 1;
        AllObjectives[CurrentObjective].SetActive(true);
    }
}
