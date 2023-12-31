/*
THIS SCRIPT WILL BE USED ACROSS ALL LEVEL SCENES.
IT SHOULD NOT HAVE ANY DEPENDENCIES.
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNameFetcher : MonoBehaviour
{
    public static string last_level_played;
    public static int next_scene_index;
    public static bool is_final_level;

    void Start()
    {
        // grab current scene name to be used in "Play Again" button
        last_level_played = SceneManager.GetActiveScene().name;

        // grab current scene index and add 1 -- to be used in "Next Level" button
        next_scene_index = SceneManager.GetActiveScene().buildIndex + 1;

        // set is_final_level
        if (last_level_played is "Level 4" or "Level V4" or "Multilevel 1-3" or "Multilevel 2-3" or "Multilevel 3-3" or "Multilevel 4-3")
        { 
            is_final_level = true;
        }
        
        else
        {
            is_final_level = false;
        }
    }
}
