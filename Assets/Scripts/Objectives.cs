using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Objectives : MonoBehaviour
{
    public int CurrentObjective;

    [Header("UI")]
    public Text Header;
    public Text Description;

    [Space]
    public int AliensRescued;
    public int MaxAliens;

    [Space]
    public Menus MenuScript;

    [System.Serializable]
    public class objctvs
    {
        [TextArea(1, 1)]
        public string ObjectiveName;
        [TextArea(1, 2)]
        public string ObjectiveDescription;
        [Space]
        public GameObject ObjectiveMarker;
        public GameObject ObjectiveDots;
    }

    [Space]
    public objctvs[] AllObjectives; //Hlutirnir


    private bool InBunker;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        AllObjectives[CurrentObjective].ObjectiveMarker.SetActive(true);
        Header.text = AllObjectives[CurrentObjective].ObjectiveName;
        Description.text = AllObjectives[CurrentObjective].ObjectiveDescription;
        print(AllObjectives.Length);
    }

    private void Update()
    {
        if (cam == null) CustomStart();// Þetta er til þess að keyra CustomStart þegar það er búið að skipta um scene
        if(CurrentObjective == 3)
            Description.text = AllObjectives[CurrentObjective].ObjectiveDescription + " " + AliensRescued + "/" + MaxAliens;
    }

    //Virkar eins og Start, nema þetta keyrir í hvert sinn sem það er skipt um scene
    void CustomStart()
    {
        cam = Camera.main;

        // Þetta disablar objectivin þegar spilarinn er í bunker
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            InBunker = false;
            AllObjectives[CurrentObjective].ObjectiveMarker.SetActive(true);
            if (AllObjectives[CurrentObjective].ObjectiveDots != null)
                AllObjectives[CurrentObjective].ObjectiveDots.SetActive(true);
        }
        else
        {
            InBunker = true;
            AllObjectives[CurrentObjective].ObjectiveMarker.SetActive(false);
            if (AllObjectives[CurrentObjective].ObjectiveDots != null)
                AllObjectives[CurrentObjective].ObjectiveDots.SetActive(false);
        }
    }

    public void ObjectiveFinished()
    {
        if (CurrentObjective +1 >= AllObjectives.Length)
            MenuScript.GameFinished();
        else
        {
            CurrentObjective += 1;

            if (InBunker == false)
            {
                AllObjectives[CurrentObjective - 1].ObjectiveMarker.SetActive(false);
                if(AllObjectives[CurrentObjective - 1].ObjectiveDots != null) AllObjectives[CurrentObjective - 1].ObjectiveDots.SetActive(false);
                AllObjectives[CurrentObjective].ObjectiveMarker.SetActive(true);
                if (AllObjectives[CurrentObjective].ObjectiveDots != null) AllObjectives[CurrentObjective].ObjectiveDots.SetActive(true);
            }

            Header.text = AllObjectives[CurrentObjective].ObjectiveName;
            Description.text = AllObjectives[CurrentObjective].ObjectiveDescription;
        }
    }
}
