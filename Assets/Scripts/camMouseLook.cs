using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour
{

    public float sensitivity = 5;
    //public float LimitY = 60F;

    private GameObject character; //Spilarinn


    private Vector2 mouseLook;
    private Vector2 rot;


    void Start()
    {
        character = this.transform.parent.gameObject;
        LockCursor(); //Læsir músina
    }

    void Update()
    {
        Vector2 vec = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")); //Nær í staðsetningu músarinar
        //Reiknar hraða og staðsetningu músar á skjánum
        vec = Vector2.Scale(vec, new Vector2(sensitivity, sensitivity));
        rot.x = Mathf.Lerp(rot.x, vec.x, 1);
        rot.y = Mathf.Lerp(rot.y, vec.y, 1);
        mouseLook += rot;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right); //Snýr myndavélina
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up); //Snýr spilaranum

        if (Input.GetKeyDown(KeyCode.Escape)) //Ef maður ýtir á "escape" þá aflæsist músin
            UnlockCursor();
        if (Input.GetMouseButtonDown(0)) //Ef maður smellir aftur á skjáinn þá læsist hún aftur
            LockCursor();
    }
    
    //Læsir músina í miðju skjáss og lætur hana hverfa
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //Aflæsir músina í miðju skjáss og lætur hana sjást
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
