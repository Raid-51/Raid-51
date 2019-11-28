using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public int gunRange;
    public int gunDamage;

    public LayerMask Mask;

    [Space]
    public GameObject Crosshair;
    public GameObject AmmoText;
    public Text AmmoTextAmount;
    public int Ammo;

    private GameObject player;
    private Camera playerCamera;
    private Inventory INV;
    private Interface IF;

    //Virkar eins og Start, nema þetta keyrir í hvert sinn sem það er skipt um scene
    private Camera cam;
    void CustomStart()
    {
        cam = Camera.main;

        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = player.GetComponentInChildren<Camera>();
        IF = GameObject.FindGameObjectWithTag("Interface").GetComponent<Interface>();
        INV = IF.gameObject.GetComponentInChildren<Inventory>();
    }
    void Update()
    {
        // Þetta er til þess að keyra CustomStart þegar það er búið að skipta um scene
        if (cam == null) CustomStart();

        AmmoTextAmount.text = Ammo + "/10";

        if (INV.CurrentItemID() == 3) //Ef spilarinn er ða halda á byssu
        {
            Crosshair.SetActive(true);
            AmmoText.SetActive(true);

            if (Input.GetMouseButtonDown(0) && Ammo > 0)
            {
                Ammo -= 1;

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
        else
        {
            Crosshair.SetActive(false);
            AmmoText.SetActive(false);
        }
    }
}
