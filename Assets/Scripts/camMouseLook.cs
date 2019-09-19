using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour
{

    public float sensitivity = 5;
    //public float LimitY = 60F;

    private GameObject character;


    private Vector2 mouseLook;
    private Vector2 rot;


    void Start()
    {
        character = this.transform.parent.gameObject;
        LockCursor();
    }

    void Update()
    {
        Vector2 vec = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        vec = Vector2.Scale(vec, new Vector2(sensitivity, sensitivity));
        rot.x = Mathf.Lerp(rot.x, vec.x, 1);
        rot.y = Mathf.Lerp(rot.y, vec.y, 1);
        mouseLook += rot;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

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