using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10.0f;
    private float translation;
    private float straffe;

    public int JumpSpeed = 50;

    private bool canJump;
    private Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        translation = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);

        if(Input.GetKeyDown(KeyCode.Space))
            RB.AddForce(0, JumpSpeed, 0, ForceMode.Impulse);
    }
}