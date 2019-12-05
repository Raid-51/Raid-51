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

    [Space] //Geymir hversu margar geimverur þurfa að vera bjargaðar og hveru margar eru nú þegar
    public int AliensRescued;
    public int MaxAliens;

    [Space]
    public Menus MenuScript;

    [System.Serializable] //Geymir öll objectives
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
            AllObjectives[CurrentObjective].ObjectiveMarker.SetActive(true);// Kveikja á current objective punktinum
            if (AllObjectives[CurrentObjective].ObjectiveDots != null)// Kveikja á objective dots ef það er verið að nota þá
                AllObjectives[CurrentObjective].ObjectiveDots.SetActive(true);
        }
        else
        {
            InBunker = true;
            AllObjectives[CurrentObjective].ObjectiveMarker.SetActive(false);// Slökkva á current objective punktinum
            if (AllObjectives[CurrentObjective].ObjectiveDots != null)// Slökkva á objective dots ef það er verið að nota þá
                AllObjectives[CurrentObjective].ObjectiveDots.SetActive(false);
        }
    }

    public void ObjectiveFinished() //Þegar maður klárar objective
    {
        if (CurrentObjective +1 >= AllObjectives.Length) //Ef spilarinn er búinn með öll objective-in þá klárast leikurinn
            MenuScript.GameFinished();
        else //Annars þá hoppar það í næsta objective
        {
            CurrentObjective += 1;

            if (InBunker == false) //Ef spilarinn fer inn í bunker þá hverfa objective-in
            {
                AllObjectives[CurrentObjective - 1].ObjectiveMarker.SetActive(false);
                if(AllObjectives[CurrentObjective - 1].ObjectiveDots != null) AllObjectives[CurrentObjective - 1].ObjectiveDots.SetActive(false);
                AllObjectives[CurrentObjective].ObjectiveMarker.SetActive(true);
                if (AllObjectives[CurrentObjective].ObjectiveDots != null) AllObjectives[CurrentObjective].ObjectiveDots.SetActive(true);
            }

            //Texti í horninu sem gefa meiri upplýsingar um objective-ið sem er í gangi
            Header.text = AllObjectives[CurrentObjective].ObjectiveName;
            Description.text = AllObjectives[CurrentObjective].ObjectiveDescription;
        }
    }
}
