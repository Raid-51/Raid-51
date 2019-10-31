using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("Pause")]
    public GameObject PauseMenu;
    private bool Paused;

    [Header("Death")]
    public GameObject DeathMenu;
    public bool immortal;
    private bool IsDead;

    private Player PlayerScript;
    private CameraLook Cam;

    void Start()
    {
        PlayerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        Cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraLook>();
        PauseAndUnPause(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            if (!Paused) PauseAndUnPause(true);

        if (!immortal)
        {
            if (PlayerScript.Health <= 0)
                StartCoroutine(Dead());
        }
    }
    public void PauseAndUnPause(bool hmm)
    {
        Time.timeScale = 1;
        Paused = hmm;
        if (Paused)
        {
            Time.timeScale = 0;
            if (!IsDead) PauseMenu.SetActive(true);
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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator Dead()
    {
        IsDead = true;
        yield return new WaitForSeconds(0.1f);
        DeathMenu.SetActive(true);
        PauseAndUnPause(true);
    }
}
