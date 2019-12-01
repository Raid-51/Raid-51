using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    public List<GameObject> AllPickups;
    public List<int> CollectedItemsFromScene;
    private List<SwitchSceneDoor> AllClosedDoors;

    [Header("Hand")]
    public int Slot1ItemID = -1;
    public int Slot2ItemID = -1;
    public int Slot3ItemID = -1;

    [HideInInspector]
    public string NextSpawnLocationName;
    [HideInInspector]
    public float LastSceneHealth;
    [HideInInspector]
    public float LastSceneStamina;

    //Virkar eins og Start, nema þetta keyrir í hvert sinn sem það er skipt um scene
    private Camera cam;
    void CustomStart()
    {
        cam = Camera.main;
        Scene scene = SceneManager.GetActiveScene();
        // Bæta öllum pickupable hlutunum í AllPickups listann ef það er ekki búið að bæta objectunum í þessu scene í listann, annars eyðir þetta öllum pickupable hlutunum
        if ( !CollectedItemsFromScene.Contains(scene.buildIndex) ) {

            // Passa að það verði ekki bætt hlutunum úr þessu scene-i í AllPickups aftur
            CollectedItemsFromScene.Add(scene.buildIndex);

            // Bæta öllum hlutunum með tagið Object við í AllPickups listann og passa að þeim verði ekki eytt þegar það er skipt um scene.
            foreach (GameObject Pickup in GameObject.FindGameObjectsWithTag("Object"))
                // Þetta gerist bara ef hluturinn er ekki í Don't Destroy On Load núþegar
                if (Pickup.scene.buildIndex != -1)
                {
                    AllPickups.Add(Pickup);
                    DontDestroyOnLoad(Pickup);
                }
        }
        else
            foreach (GameObject Pickup in GameObject.FindGameObjectsWithTag("Object"))
                if (Pickup.scene.buildIndex != -1) Destroy(Pickup);

        // Eyða öllum pickupable hlutunum sem byrjuðu í sceninu en eiga ekki að vera lengur því að leikurinn er núþegar að geyma þá
        foreach (GameObject Pickupable in GameObject.FindGameObjectsWithTag("Object"))
            if (!AllPickups.Contains(Pickupable))
                Destroy(Pickupable);

        // Slökkva og kveikja á pickupable objectum eftir því í hvaða sceni þeir eiga að vera
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        foreach (GameObject Pickupable in AllPickups)
        {
            // Ef hluturinn segir að hann eigi að vera í þessu scenei þá er hann activataður, annars er hann deactivataður
            if (Pickupable.GetComponent<Object>().SceneNumber == currentScene)
            {
                // Færa hlutinn aðeins upp svo að hann detti ekki í gegnum terrainið
                Vector3 location = Pickupable.transform.position;
                location.y += 0.05f;
                Pickupable.transform.position = location;

                // Activata hlutinn aftur
                Pickupable.SetActive(true);
            }
            else Pickupable.SetActive(false);
        }

        // Finna spilarann til þess að gefa honum rétt líf og stamina og líka til þess að mögulega hreyfa hann
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // Teleporta spilaranum á staðinn sem er í NextSpawnLocationName ef það er eitthvað í NextSpawnLocationName
        if (NextSpawnLocationName != "") {
            Debug.Log("NextSpawnLocationName: "+NextSpawnLocationName);
            Transform playerTransform = player.gameObject.GetComponent<Transform>();
            Transform teleportTransform = GameObject.Find(NextSpawnLocationName).GetComponent<Transform>();

            playerTransform.position = teleportTransform.position;
            playerTransform.rotation = teleportTransform.rotation;

            // Close the bunker the player is exiting
            if (NextSpawnLocationName.Substring(0, 7) == "Bunker ")
            {
                Debug.Log("Closing Bunker Door");
                SwitchSceneDoor SSD = teleportTransform.gameObject.GetComponentInParent<SwitchSceneDoor>();
                AllClosedDoors.Add(SSD);
            }

            // Hreinsa NextSpawnLocationName
            NextSpawnLocationName = "";
        }

        // Gefa spilaranum rétt líf og stamina
        player.Health = LastSceneHealth;
        player.Stamina = LastSceneStamina;

        // Gefa spilaranum réttu hlutina í höndina
        Hand playerHand = player.gameObject.GetComponent<Hand>();

        if (Slot1ItemID != -1) playerHand.AddItem(Slot1ItemID, 1);
        if (Slot2ItemID != -1) playerHand.AddItem(Slot2ItemID, 2);
        if (Slot3ItemID != -1) playerHand.AddItem(Slot3ItemID, 3);

        // Loka öllum hurðum sem eru í AllClosedDoors listanum
        if (scene.buildIndex == 0) foreach (SwitchSceneDoor SSD in AllClosedDoors) SSD.CloseDoor();
    }

    void Update()
    {
        // Þetta er til þess að keyra CustomStart þegar það er búið að skipta um scene
        if (cam == null) CustomStart();
    }
}