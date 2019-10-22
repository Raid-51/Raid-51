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
        PlayerSpeed(); //Stillir hraða spilarans

        //Hreyfir spilarann
        translation = Input.GetAxis("Vertical") * CurrentSpeed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * CurrentSpeed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);
        
        //Ef spilarinn er á jörðu
        if (Grounded)
            if (Stamina > 10) //Og er með nóg þol
                if (Input.GetKeyDown(KeyCode.Space)) //Og ýtir á "space"
                {
                    RB.AddForce(0, JumpSpeed, 0, ForceMode.Impulse); //L´tur spilarann hoppa
                    Stamina -= 10;
                }

        //Sýnir þol og líf spilarans á skjánum (interface)
        IF.Stamina.value = Stamina;
        IF.Health.value = Health;
    
        if (Stamina < 0) Stamina = 0; //Ef þolið er komið undir 0 þá er það sett á 0, á ekki að geta farið undir það
        if (Stamina > IF.Stamina.maxValue) Stamina = IF.Stamina.maxValue; //Ef þolið er komið yfir 100 þá er það sett á 100, á ekki að geta yfir það
    }

    void FixedUpdate()
    {
        //Skýtur raycast niður
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), -Vector3.up, out hit, 2))
        {
            if (hit.distance < 1.1f) //Ef það hittir jörðina þá er spilarinn líka á jörðunni
                Grounded = true;
            else //Annars er hann það ekki
                Grounded = false;
        }
    }
    //Stillir hraða spilarans
    void PlayerSpeed()
    {
        //Ef hann er á jörðunni
        if (Grounded)
        {
            //Og er hlaupandi
            if (Input.GetButton("Run"))
            {
                //Og er með nóg þol
                if (Stamina > 0.3)
                {
                    CurrentSpeed = RunSpeed; //Þá hleypur hann
                    Stamina -= 0.5f; //Minnkar þolið
                }
                else //Annars ekki
                    CurrentSpeed = Speed;
            }
            else //Ef hann er ekki hlaupandi þá gengur hann á venjulegum hraða
            {
                CurrentSpeed = Speed;
                Stamina += 0.2f; //Aukar þolið
            }
            //Ef hann er að crouch-a 
            if (Input.GetButton("Crouch"))
            {
                CurrentSpeed = CurrentSpeed * 0.4f; //Hraðinn minnkar
                transform.localScale = new Vector3(1, 0.5f, 1); //Spilarinn minkar niður
            }
            //Ef hann er ekki að crouch-a
            else
                transform.localScale = new Vector3(1, 1, 1); //Spilarinn stækkar aftur

            //Ef spilarinn labbar á afturábak eða til hliðar þá labbar hann hægar
            if (Input.GetButton("Horizontal") || Input.GetKey(KeyCode.S))
                CurrentSpeed = CurrentSpeed * 0.5f;
        }
        //Ef hann er ekki á jörðunni þá fer hann með lofthraða
        else
        {
            if (Input.GetButton("Run"))
                CurrentSpeed = AirSpeed * 2;
            else
                CurrentSpeed = AirSpeed;
        }
    }
    //Spilarinn getur misst líf
    public void TakeDamage(float Damage)
    {
        Health -= Damage;
    }

    /*void OnCollisionEnter(Collision collision)
    {
        RB.velocity = Vector3.zero;
    }*/
}
