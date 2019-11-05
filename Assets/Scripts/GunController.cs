using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public int gunRange;
    public int gunDamage;

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
        if (Input.GetMouseButtonDown(0) && INV.CurrentItemID() == 3)
        {
            int layerMask = ~(1 << 11);
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, layerMask))
            {
                //Ef það hittir óvin
                if (hit.collider.tag == "Enemy")
                {
                    GameObject enemyHit = hit.collider.gameObject;
                    NewEnemy enemyHitScript = enemyHit.GetComponent<NewEnemy>();

                    enemyHitScript.Health -= gunDamage;

                    // Tékka hvort að óvinurinn eigi að deyja
                    if (enemyHitScript.Health <= 0) enemyHitScript.Dead();
                    else enemyHitScript.ChasePlayer();
                }
            }
        }
    }
}
