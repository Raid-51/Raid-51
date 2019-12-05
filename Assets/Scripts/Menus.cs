using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    //Pásuskjár
    [Header("Pause")]
    public GameObject PauseMenu;
    public bool disablePauseScreen;
    private bool Paused;

    //Dauðaskjár
    [Header("Death")]
    public GameObject DeathMenu;
    public bool immortal;
    private bool IsDead;

    //Dauðaskjár
    [Header("Finish")]
    public GameObject FinishMenu;

    private Player PlayerScript;
    private CameraLook Cam;

    //Save-ar menu-in, virkar sem Start
    void OnEnable() { SceneManager.sceneLoaded += CustomStart; }
    void CustomStart(Scene scene, LoadSceneMode mode)
    {
        PlayerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        Cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraLook>();
        PauseAndUnPause(false);
    }

    void Update()
    {
        //Ef spilarinn lokar pásuskjánum eða ýtir á esc, þá byrjar leikurinn aftur
        if (Input.GetButtonDown("Pause") && disablePauseScreen == false) 
            if (!Paused) PauseAndUnPause(true);

        if (!immortal) //Fyrir debugging svo að spilarinn séi ekki að deyja endalaust
        {
            if (PlayerScript.Health <= 0)
                StartCoroutine(Dead());
        }
    }
    //Pásar of af-pásar leiknum
    public void PauseAndUnPause(bool hmm)
    {
        Time.timeScale = 1;
        Paused = hmm;
        if (Paused) //Ef það er pása þá frystir það allt
        {
            Time.timeScale = 0;
            if (!IsDead) PauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerScript.enabled = false;
            Cam.enabled = false;
        }
        else //Ef það er ekki pása þá af-frystir það allt
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerScript.enabled = true;
            Cam.enabled = true;
        }
    }

    public void Restart() //Restartar leiknum
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() //Fer í main menu (byrjunarskjáinn)
    {
        SceneManager.LoadScene("Start");
    }

    public IEnumerator Dead() //Ef spilarinn deyr, þá frystir það leikinn og sýnir dauðaskjáinn
    {
        IsDead = true;
        yield return new WaitForSeconds(0.1f);
        DeathMenu.SetActive(true);
        PauseAndUnPause(true);
    }

    public void GameFinished() //Þegar leikinum er lokið þá frystist leikurinn og lokaskjárinn er sýndur
    {
        FinishMenu.SetActive(true);
        PauseAndUnPause(true);
    }
}
