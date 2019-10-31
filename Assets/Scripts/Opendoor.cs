using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opendoor : MonoBehaviour
{
    [Header("NOT REQUIRED! (Anim is for cell door)")]
    public SwitchSceneDoor SceneDoor;

    public Animator Anim;

    public void Open()
    {
        if (SceneDoor == null)
            Anim.SetBool("Open", true);
        else
            SceneDoor.Locked = false;
    }

}
