using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("Sensativity")]
    public float Sensitivity = 5; //hraði myndavélarinnar

    [Header("Limits")]
    public float LimitY = 60F;

    private GameObject character;

    float rotationY = 0F;
    private Vector2 mouseLook;
    private Vector2 rot;

    void Start()
    {
        character = this.transform.parent.gameObject;
        LockCursor();
    }


    void Update()
    {
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity; //Nær í hraða sem spilarinn fer til hægri og vinstri

        rotationY += Input.GetAxis("Mouse Y") * Sensitivity; //Nær í hraða sem spilarinn fer til up og niður
        rotationY = Mathf.Clamp(rotationY, -LimitY, LimitY); //Stoppar myndavélina að snúast of langt upp eða niður

        transform.localEulerAngles = new Vector3(-rotationY, 0, 0); //Snýr myndavélinni

        //if (Input.GetKeyDown(KeyCode.Escape))
            //UnlockCursor();
        //if (Input.GetMouseButtonDown(0))
            //LockCursor();

        RotateCharacter(); //Snýr spilaranum

    }
    //Læsir músinni í miðju skjáss
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //Aflæsir músinni
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    //Snýr spilaranum með myndavélinni
    void RotateCharacter()
    {
        Vector2 vec = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")); //Nær í staðsetningu músarinar
        //Reiknar hraða og staðsetningu músar á skjánum
        vec = Vector2.Scale(vec, new Vector2(Sensitivity, Sensitivity));
        rot.x = Mathf.Lerp(rot.x, vec.x, 1);
        rot.y = Mathf.Lerp(rot.y, vec.y, 1);
        mouseLook += rot;

        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up); //Snýr spilaranum
    }
}
