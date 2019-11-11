using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opendoor : MonoBehaviour
{
    [Header("NOT REQUIRED! (Anim is for cell door)")]
    public SwitchSceneDoor SceneDoor; //Ef þetta er tómt þá er þetta bara venjuleg hurð annars er þetta fangelsishurð

    public Animator Anim;

    public void Open()
    {
        if (SceneDoor == null)
            Anim.SetBool("Open", true); //Opnar fangelsishurð
        else 
            SceneDoor.Locked = false; //Aflæsir scenehurð
    }

}
