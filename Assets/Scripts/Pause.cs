using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    
    private bool Paused;
    private Player PlayerScript;
    private CameraLook Cam;

    void Start()
    {
        PlayerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        Cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraLook>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            if (!Paused) PauseAndUnPause(true);
    }

    public void PauseAndUnPause(bool hmm)
    {
        Time.timeScale = 1;
        Paused = hmm;
        if (Paused)
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerScript.enabled = false;
            Cam.enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerScript.enabled = true;
            Cam.enabled = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
