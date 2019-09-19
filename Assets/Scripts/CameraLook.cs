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

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        //character.transform.localRotation = Quaternion.AngleAxis(rotationX, character.transform.up);

        if (Input.GetKeyDown(KeyCode.Escape))
            UnlockCursor();
        if (Input.GetMouseButtonDown(0))
            LockCursor();

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
}
