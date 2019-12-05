using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startmenu : MonoBehaviour
{
    //Þessi script er fyrir byrjunarskjáinn

    public void StartGame() //Byrjar leikin
    {
        SceneManager.LoadScene("Eyðimörk");
    }

    public void MainMenu() //Fer í main menu (byrjunarskjáinn)
    {

    }

    public void QuitGame() //Hættir leiknum og slekkur á honum
    {
        Application.Quit();
    }
}
