using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public void GoToSinglePlayerLevelSelect()
    {
        SceneManager.LoadScene("Single Player Level Select");
    }

    public void GoToVersusModeSelect()
    {
        SceneManager.LoadScene("Versus Mode Select");
    }

    public void GoToVersusClassicLevelSelect()
    {
        SceneManager.LoadScene("Versus Classic Level Select");
    }

    public void GoToVersusMultilevelLevelSelect()
    {
        SceneManager.LoadScene("Versus Multilevel Level Select");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void LoadLevelV1()
    {
        SceneManager.LoadScene("Level V1");
    }

    public void LoadLevelV2()
    {
        SceneManager.LoadScene("Level V2");
    }

    public void LoadLevelV3()
    {
        SceneManager.LoadScene("Level V3");
    }

    public void LoadLevelV4()
    {
        SceneManager.LoadScene("Level V4");
    }

    public void LoadMultilevel1_1()
    {
        SceneManager.LoadScene("Multilevel 1-1");
    }

    public void LoadMultilevel1_2()
    {
        SceneManager.LoadScene("Multilevel 1-2");
    }

    public void LoadMultilevel1_3()
    {
        SceneManager.LoadScene("Multilevel 1-3");
    }

    public void LoadMultilevel2_1()
    {
        SceneManager.LoadScene("Multilevel 2-1");
    }

    public void LoadMultilevel2_2()
    {
        SceneManager.LoadScene("Multilevel 2-2");
    }

    public void LoadMultilevel2_3()
    {
        SceneManager.LoadScene("Multilevel 2-3");
    }

    public void LoadMultilevel3_1()
    {
        SceneManager.LoadScene("Multilevel 3-1");
    }

    public void LoadMultilevel3_2()
    {
        SceneManager.LoadScene("Multilevel 3-2");
    }

    public void LoadMultilevel3_3()
    {
        SceneManager.LoadScene("Multilevel 3-3");
    }

    public void LoadMultilevel4_1()
    {
        SceneManager.LoadScene("Multilevel 4-1");
    }

    public void LoadMultilevel4_2()
    {
        SceneManager.LoadScene("Multilevel 4-2");
    }

    public void LoadMultilevel4_3()
    {
        SceneManager.LoadScene("Multilevel 4-3");
    }

    public void VersusModeStart()
    {
        SceneManager.LoadScene("Level V1");
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene($"{SceneNameFetcher.last_level_played}"); 
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GoToHelpScreen()
    {
        SceneManager.LoadScene("Help Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
