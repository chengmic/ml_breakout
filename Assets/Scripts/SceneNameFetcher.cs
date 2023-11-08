/*
THIS SCRIPT WILL BE USED ACROSS ALL LEVEL SCENES.
IT SHOULD NOT HAVE ANY DEPENDENCIES.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNameFetcher : MonoBehaviour
{
    public static string last_level_played;

    void Start()
    {
        // grab current scene name to be used in "Play Again" button
        last_level_played = SceneManager.GetActiveScene().name;
    }
}
