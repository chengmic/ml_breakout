using UnityEngine;
using TMPro;

public class TrainingManager : MonoBehaviour
{
    public int lives_lost;
    public int total_lives_lost = 0;
    public int games_won = 0;
    public TextMeshProUGUI lives_lost_text;
    public TextMeshProUGUI total_lives_lost_text;
    public TextMeshProUGUI games_won_text;
    public int total_bricks = 0;
    public int bricks_remaining;
    public TrainingArea game_area;

    void Start()
    {
        DisplayLives();
        DisplayTotalLives();
        DisplayGamesWon();

        // track bricks in level
        total_bricks = game_area.TotalBricks();
        bricks_remaining = total_bricks;
    }

    public void DisplayLives()
    {
        lives_lost_text.text = "Lives Lost: " + lives_lost;
    }

    public void DisplayTotalLives()
    {
        total_lives_lost_text.text = "Total Lost: " + total_lives_lost;
    }

    public void DisplayGamesWon()
    {
        games_won_text.text = "Games Won: " + games_won;
    }

    public void ChangeLives(int life_change)
    {
        lives_lost += life_change;
        total_lives_lost += life_change;
        
        DisplayLives();
        DisplayTotalLives();
    }

    public void ReduceBrickCount()
    {
        bricks_remaining -= 1;

        if (bricks_remaining == 0) 
        {
                lives_lost = 0;
                DisplayLives();

                games_won++;
                DisplayGamesWon();

                bricks_remaining = total_bricks;
                game_area.ResetArea();
        }
    }
}
