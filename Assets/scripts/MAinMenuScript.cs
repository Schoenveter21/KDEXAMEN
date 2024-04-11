using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAinMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Helpmenu()
    {
        SceneManager.LoadScene("Hekp");
    }
    public void GOBACK()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
