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
        // Sækja króka í nauðsinlegar scriptur eða Game Objects
        IFGameObject = GameObject.FindGameObjectWithTag("Interface");
        SSM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchSceneManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Interaction scriptið kallar á þetta til þess að skipta um Scene
    public void SwitchScene()
    {
        // Slökkva á collider svo að það sé ekki hægt að tvísmella á hurðina
        Collider collider = this.GetComponent<BoxCollider>();
        collider.enabled = false;

        // Passa að Interfaceinu sé ekki eytt þegar það er skipt um scene
        if (IFGameObject.scene.buildIndex != -1)
            DontDestroyOnLoad(IFGameObject);

        // Setja næsta spawn stað í Switch Scene Managerinn ef það er specify-að spawn stað fyrir næsta scene,
        // fyrir bunkerana þurfum við ekki að specifya spawn stað fyrst að það er bara ein leið inn en fyrir 
        // eyðimörkina þarf að specifya spawn stað fyrst að spilarinn gæti veriða að spawna í fyrsta sinn
        // eða koma úr eitthverjum bunker.
        if (SpawnName != "") SSM.NextSpawnLocationName = SpawnName;

        // Færa líf og stamina spilaranns á Switch Scene Managerinn til þess að það geti verið það sama í næsta scene-i
        SSM.LastSceneHealth = player.Health;
        SSM.LastSceneStamina = player.Stamina;

        // Skipta um scene
        SceneManager.LoadScene(SceneNumber);
    }
}