using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    public GameObject NextLevelButton;
    public GameObject YouWinBackground;
    public GameObject CongratsBackground;

    // Start is called before the first frame update
    void Start()
    {
        // check if the last scene is the final level in the set
        if (SceneNameFetcher.is_final_level)
        {
            // remove 'next level' button
            NextLevelButton.SetActive(false);

            // change background from 'you win' to 'congratulations'
            YouWinBackground.SetActive(false);
            CongratsBackground.SetActive(true);

            // reset is_final_level
            SceneNameFetcher.is_final_level = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
