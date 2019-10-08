using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float SightRange;

    private Transform player;

    void Start()
    {

    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //player = other.transform;


            //if (Physics.CheckSphere(transform.position, SightRange))
            //{
            //    print("found you");
            //}

            /*RaycastHit hit;
            Ray ray = new Ray(transform.position, player.transform.position);
            Debug.DrawRay(ray.origin, ray.direction, Color.blue);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    print("found you");
                }
            }*/
        }
    }
}
