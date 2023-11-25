using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerGameManager : MonoBehaviour
{
    public int lives_val;
    public int score_val;
    public TextMeshProUGUI lives_text;
    public TextMeshProUGUI score_text;
    public GameObject lost_game_panel;
    public GameObject won_game_panel;
    public int total_bricks = 0;
    public int bricks_remaining;
    public PlayerGameArea player_game_area;

    private float color_change_duration = 0.5f; // In seconds

    void Start()
    {
        // display score and lives
        DisplayScore();
        DisplayLives();

        // track bricks in level
        total_bricks = player_game_area.TotalBricks();
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

    public void LifeIncreased(){
        score_text.text = "Score: " + score_val + " + 1";
    }

    public void ChangeLives(int life_change)
    {
        lives_val += life_change;
        if (lives_val <= 0)
        {
            LoseGame();
        }

        if (life_change <0){
            StartCoroutine(ChangeTextColor(lives_text, Color.red));
        }
        else{
            StartCoroutine(ChangeTextColor(lives_text, Color.green));
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
            WinGame();
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

    // Method used to chanage color of text in play screen. 
    public IEnumerator ChangeTextColor(TextMeshProUGUI TextToChange, Color NewColor)
    {
        TextToChange.color = NewColor;

        yield return new WaitForSeconds(color_change_duration);

        TextToChange.color = Color.white;;

    }

}
