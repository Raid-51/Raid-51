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

        SceneManager.LoadScene(SceneNumber);
    }
}