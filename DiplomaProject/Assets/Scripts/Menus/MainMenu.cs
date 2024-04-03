using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        if(!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetString("CurrentLevel", "Level 1");
        }
        var currentLevelName = PlayerPrefs.GetString("CurrentLevel");
        SceneManager.LoadScene(currentLevelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
