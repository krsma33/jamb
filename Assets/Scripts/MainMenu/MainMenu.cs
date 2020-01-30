using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlaySinglePlayerGame()
    {
        SceneManager.LoadScene("SinglePlayerGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
