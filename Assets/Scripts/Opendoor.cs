using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opendoor : MonoBehaviour
{
    public Animator Anim;

    public void Open()
    {
        Anim.SetBool("Open", true);
    }

}
