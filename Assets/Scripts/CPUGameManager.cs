using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CPUGameManager : MonoBehaviour
{
    public int lives_val;
    public int score_val;
    public TextMeshProUGUI lives_text;
    public TextMeshProUGUI score_text;
    public GameObject lost_game_panel;
    public GameObject won_game_panel;
    public int total_bricks = 0;
    public int bricks_remaining;
    public CPUGameArea game_area;

    void Start()
    {
        // display score and lives
        DisplayScore();
        DisplayLives();

        // track bricks in level
        //total_bricks = FindObjectsOfType<Brick>().Length;
        total_bricks = game_area.TotalBricks();
        //Debug.Log(total_bricks);
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

        // if CPU loses all lives, Player wins game
        if (lives_val <= 0)
        {
            WinGame();
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

        // if CPU destroys all brick, Player loses game
        if (bricks_remaining == 0)
        {
            bricks_remaining = total_bricks;
            LoseGame();
        }
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Win Screen");
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("Game Over Screen");
    }
}
