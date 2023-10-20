using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pause_overlay;
    public GameObject game_ui;
    private bool paused = false;

    
    // Start is called before the first frame update
    void Start()
    {
        pause_overlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // pause game if escape key is hit and go to pause screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                PauseTheGame();
            }
            else
            {
                ResumeGame();
            }
            
        }
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0;
        paused = true;
        pause_overlay.SetActive(true);
        game_ui.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
        pause_overlay.SetActive(false);
        game_ui.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
