using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int lives_val;
    public int score_val;
    public TextMeshProUGUI lives_text;
    public TextMeshProUGUI score_text;



    // Start is called before the first frame update
    void Start()
    {
        display_score();
        display_lives();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void display_lives()
    {
        lives_text.text = "Lives: " + lives_val;
    }

    public void display_score()
    {
        score_text.text = "Score: " + score_val;
    }

    public void changeLives(int life_change)
    {
        lives_val += life_change;

        display_lives();
    }

    public void changeScore(int score_change) {
        score_val += score_change;
        display_score();
    }



}
