using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        transform.LookAt(2* transform.position - new Vector3(player.position.x, player.position.y + 1, player.position.z));
    }
}
