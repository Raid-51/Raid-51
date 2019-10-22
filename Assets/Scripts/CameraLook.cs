using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("Sensativity")]
    public float Sensitivity = 5;

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
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity;

        rotationY += Input.GetAxis("Mouse Y") * Sensitivity;
        rotationY = Mathf.Clamp(rotationY, -LimitY, LimitY);

        transform.localEulerAngles = new Vector3(-rotationY, 0, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
            UnlockCursor();
        if (Input.GetMouseButtonDown(0))
            LockCursor();

        RotateCharacter();

    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

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
