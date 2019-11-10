using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public int gunRange;
    public int gunDamage;

    public LayerMask Mask;

    private GameObject player;
    private Camera playerCamera;
    private Inventory INV;
    private Interface IF;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = player.GetComponentInChildren<Camera>();
        IF = GameObject.FindGameObjectWithTag("Interface").GetComponent<Interface>();
        INV = IF.gameObject.GetComponentInChildren<Inventory>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && INV.CurrentItemID() == 3) //Ef spilarinn er ða halda á byssu
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); //Skýtur raycast í miðju skjáar

            if (Physics.Raycast(ray, out hit, 20, Mask))
            {
                //Ef það hittir óvin
                if (hit.collider.tag == "Enemy")
                {
                    print("Owch");

                    GameObject enemyHit = hit.collider.gameObject;
                    NewEnemy enemyHitScript = enemyHit.GetComponent<NewEnemy>();

                    enemyHitScript.Health -= gunDamage; //Tekur líf af enemy-inum

                    // Tékka hvort að óvinurinn eigi að deyja
                    if (enemyHitScript.Health <= 0) enemyHitScript.Dead();
                    else enemyHitScript.ChasePlayer();
                }
            }
        }
    }
}
