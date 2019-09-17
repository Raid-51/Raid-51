using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("Sensativity")]
    public float Sensitivity = 5;

    [Header("Limits")]
    public float LimitY = 60F;

    float rotationY = 0F;

    void Update()
    {
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity;

        rotationY += Input.GetAxis("Mouse Y") * Sensitivity;
        rotationY = Mathf.Clamp(rotationY, -LimitY, LimitY);

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
}
