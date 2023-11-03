using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public int current_level_index;

    public void SinglePlayerStart()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void VersusModeStart()
    {
        SceneManager.LoadScene("Level V1");
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene($"{GameManager.last_level_played}"); 
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
