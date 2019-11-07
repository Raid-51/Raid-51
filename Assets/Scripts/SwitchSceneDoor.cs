using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchSceneDoor : MonoBehaviour
{
    public int SceneNumber;
    public string SpawnName;
    public int InteractionTextID;
    public bool Locked = true;

    private GameObject IFGameObject;
    private SwitchSceneManager SSM;
    private Player player;

    void Start()
    {
        IFGameObject = GameObject.FindGameObjectWithTag("Interface");
        SSM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchSceneManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SwitchScene()
    {
        Collider collider = this.GetComponent<BoxCollider>();
        collider.enabled = false;

        if (IFGameObject.scene.buildIndex != -1)
            DontDestroyOnLoad(IFGameObject);

        if (SpawnName != "") SSM.NextSpawnLocationName = SpawnName;

        SSM.LastSceneHealth = player.Health;
        SSM.LastSceneStamina = player.Stamina;

        SceneManager.LoadScene(SceneNumber);
    }
}