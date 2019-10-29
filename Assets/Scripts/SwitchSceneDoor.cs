using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchSceneDoor : MonoBehaviour
{
    public int SceneNumber;
    public int InteractionTextID;

    [HideInInspector]
    public int OldSceneScroll;
    [HideInInspector]
    public List<int> OldSceneItemID = new List<int>(3);
    [HideInInspector]
    public List<Image> OldSceneItemSprites = new List<Image>(3);

    public void SwitchScene()
    {
        Collider collider = this.GetComponent<BoxCollider>();
        collider.enabled = false;

        Inventory INV = GameObject.FindGameObjectWithTag("Interface").GetComponentInChildren<Inventory>();

        OldSceneScroll = INV.Scroll;

        OldSceneItemID.Add(INV.itemID1);
        OldSceneItemID.Add(INV.itemID2);
        OldSceneItemID.Add(INV.itemID3);

        OldSceneItemSprites.Add(INV.ItemIcon1);
        OldSceneItemSprites.Add(INV.ItemIcon2);
        OldSceneItemSprites.Add(INV.ItemIcon3);

        DontDestroyOnLoad(this.gameObject);

        this.gameObject.name = "Old Scene Player Info";

        SceneManager.LoadScene(SceneNumber);

        //Inventory NEW_INV = GameObject.FindGameObjectWithTag("Interface").GetComponentInChildren<Inventory>();

        //Debug.Log(NEW_INV);

        //NEW_INV.FillSlot(1, OldSceneItemID[0]);
        //NEW_INV.FillSlot(2, OldSceneItemID[1]);
        //NEW_INV.FillSlot(3, OldSceneItemID[2]);

        //NEW_INV.Scroll = scroll;

        //Debug.Log("Inventory fillt");
    }
}