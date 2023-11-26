using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    public GameObject next_level_button;
    public GameObject you_win_background;
    public GameObject congrats_background;

    // Start is called before the first frame update
    void Start()
    {
        // check if the last scene is the final level in the set
        if (SceneNameFetcher.is_final_level)
        {
            // remove 'next level' button
            next_level_button.SetActive(false);

            // change background from 'you win' to 'congratulations'
            you_win_background.SetActive(false);
            congrats_background.SetActive(true);

            // reset is_final_level
            SceneNameFetcher.is_final_level = false;
        }
    }
}
