using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    public List<GameObject> AllPickups;
    [Space]
    public string NextSpawnLocationName;
    public float LastSceneHealth;
    public float LastSceneStamina;

    // Þessi kóði þarf allur að keyra á undan Start methodunum
    void Awake()
    {
        // Tékka hvort að þessi Game Controller sé ekki sá fyrsti, ef hann er ekki sá fyrsti slekkur hann á sér
        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
            this.gameObject.SetActive(false);
        else
        {
            DontDestroyOnLoad(this.gameObject);// Passa að objectinum sé ekki eytt

            // Bæta öllum pickupable hlutonum í AllPickups listann
            foreach (GameObject Pickup in GameObject.FindGameObjectsWithTag("Object"))
            {
                AllPickups.Add(Pickup);
            }
        }
    }

    void OnEnable() { SceneManager.sceneLoaded += CustomStart; }
    void OnDisable() { SceneManager.sceneLoaded -= CustomStart; }

    // Þetta keyrir í hvert sinn sem það er skipt um scene
    void CustomStart(Scene scene, LoadSceneMode mode)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (NextSpawnLocationName != "") {
            Transform playerTransform = player.gameObject.GetComponent<Transform>();
            Transform teleportTransform = GameObject.Find(NextSpawnLocationName).GetComponent<Transform>();

            playerTransform.position = teleportTransform.position;
            playerTransform.rotation = teleportTransform.rotation;

            NextSpawnLocationName = "";
        }

        player.Health = LastSceneHealth;
        player.Stamina = LastSceneStamina;
    }
}