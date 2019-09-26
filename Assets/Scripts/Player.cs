using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float Health = 100;
    public float Stamina = 100;

    [Header("Speed")]
    public float Speed = 10;
    public float RunSpeed = 15;
    public float AirSpeed = 4;
    public int JumpSpeed = 10;

    private float CurrentSpeed;
    private float translation;
    private float straffe;
    private bool Grounded;
    private Rigidbody RB;
    private Interface IF;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        IF = GameObject.FindGameObjectWithTag("Interface").GetComponent<Interface>();
    }

    void Update()
    {
        PlayerSpeed();

        translation = Input.GetAxis("Vertical") * CurrentSpeed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * CurrentSpeed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);

        if (Grounded)
            if (Stamina > 10)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RB.AddForce(0, JumpSpeed, 0, ForceMode.Impulse);
                    Stamina -= 10;
                }

        IF.Stamina.value = Stamina;
        IF.Health.value = Health;

        if (Stamina < 0) Stamina = 0;
        if (Stamina > IF.Stamina.maxValue) Stamina = IF.Stamina.maxValue;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.5f))
        {
            if (hit.distance < 0.3f)
                Grounded = true;
            else
                Grounded = false;
        }
    }

    void PlayerSpeed()
    {
        if (Grounded)
        {
            if (Input.GetButton("Run"))
            {
                if (Stamina > 0.3)
                {
                    CurrentSpeed = RunSpeed;
                    Stamina -= 0.5f;
                }
                else
                    CurrentSpeed = Speed;
            }
            else
            {
                CurrentSpeed = Speed;
                Stamina += 0.2f;
            }

            if (Input.GetButton("Crouch"))
            {
                CurrentSpeed = CurrentSpeed * 0.4f;
                transform.localScale = new Vector3(1, 0.5f, 1);
            }
            else
                transform.localScale = new Vector3(1, 1, 1);

            if (Input.GetButton("Horizontal") || Input.GetKey(KeyCode.S))
                CurrentSpeed = CurrentSpeed * 0.5f;
        }
        else
            CurrentSpeed = AirSpeed;
    }

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
    }
}