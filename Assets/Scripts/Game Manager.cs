using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives_val;
    public int score_val;
    public TextMeshProUGUI lives_text;
    public TextMeshProUGUI score_text;
    public GameObject lost_game_panel;
    public GameObject won_game_panel;
    public int total_bricks;
    public int bricks_remaining;

    // Start is called before the first frame update
    void Start()
    {
        DisplayScore();
        DisplayLives();

        total_bricks = FindObjectsOfType<Brick>().Length;
        bricks_remaining = total_bricks;
    }

    public void DisplayLives()
    {
        lives_text.text = "Lives: " + lives_val;
    }

    public void DisplayScore()
    {
        score_text.text = "Score: " + score_val;
    }

    public void ChangeLives(int life_change)
    {
        lives_val += life_change;

        if (lives_val <= 0) {
            SceneManager.LoadScene("Game Over Screen");
        }
        
        DisplayLives();
    }

    public void ChangeScore(int score_change)
    {
        score_val += score_change;
        DisplayScore();
    }

    public void ReduceBrickCount()
    {
        bricks_remaining -= 1;

        if (bricks_remaining == 0) {
            SceneManager.LoadScene("Win Screen");
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
