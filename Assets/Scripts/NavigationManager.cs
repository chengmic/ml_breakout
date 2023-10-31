using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public void SinglePlayerStart()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void VersusModeStart()
    {
        SceneManager.LoadScene("Level V1");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
