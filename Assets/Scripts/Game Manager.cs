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
    public int total_bricks = 0;
    public int bricks_remaining;
    public static string last_level_played;
    public GameArea game_area;

    // Start is called before the first frame update
    void Start()
    {
        // grab current scene name to be used in "Play Again" button
        last_level_played = SceneManager.GetActiveScene().name;

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

        if (lives_val <= 0) 
        {
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

        if (bricks_remaining == 0) 
        {
            if (last_level_played == "ML Training")
            {
                bricks_remaining = total_bricks;
                game_area.ResetArea();
            }
            else
            {
                SceneManager.LoadScene("Win Screen");
            }
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void GetCurrentScene()
    {

    }
}
