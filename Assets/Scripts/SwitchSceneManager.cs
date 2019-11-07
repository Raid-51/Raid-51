using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    public List<GameObject> AllPickups;
    public List<int> CollectedItemsFromScene;

    [HideInInspector]
    public string NextSpawnLocationName;
    [HideInInspector]
    public float LastSceneHealth;
    [HideInInspector]
    public float LastSceneStamina;

    // Þessi kóði þarf allur að keyra á undan Start methodunum
    void Awake()
    {
        // Tékka hvort að þessi Game Controller sé ekki sá fyrsti, ef hann er ekki sá fyrsti slekkur hann á sér
        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
            this.gameObject.SetActive(false);
        else
            DontDestroyOnLoad(this.gameObject);// Passa að objectinum sé ekki eytt
    }

    void OnEnable() { SceneManager.sceneLoaded += CustomStart; }
    void OnDisable() { SceneManager.sceneLoaded -= CustomStart; }

    // Þetta keyrir í hvert sinn sem það er skipt um scene
    void CustomStart(Scene scene, LoadSceneMode mode)
    {
        // Bæta öllum pickupable hlutunum í AllPickups listann ef það er ekki búið að bæta objectunum í þessu scene í listann
        if ( !CollectedItemsFromScene.Contains(scene.buildIndex) ) {

            CollectedItemsFromScene.Add(scene.buildIndex);

            foreach (GameObject Pickup in GameObject.FindGameObjectsWithTag("Object"))
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

        // Teleporta spilaranum á staðinn sem er í NextSpawnLocationName
        if (NextSpawnLocationName != "") {
            Transform playerTransform = player.gameObject.GetComponent<Transform>();
            Transform teleportTransform = GameObject.Find(NextSpawnLocationName).GetComponent<Transform>();

            playerTransform.position = teleportTransform.position;
            playerTransform.rotation = teleportTransform.rotation;

            NextSpawnLocationName = "";
        }

        // Gefa spilaranum rétt líf og stamina
        player.Health = LastSceneHealth;
        player.Stamina = LastSceneStamina;
    }
}