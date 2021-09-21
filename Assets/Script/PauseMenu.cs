using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePause = false;
    public GameObject pauseMenu;
    //public GameObject StopGame;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePause)
            {
                Resume();
            }
            else
            {
                pausee();
            }
        }
    }

    public void ButtonPause()
    {
        if( GamePause)
        {
            Resume();
        }
        else
        {
            pausee();
        }
    }

    public void Resume()
    {
       // StopGame.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePause = false;
    }

    public void pausee()
    {
       // StopGame.SetActive(true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
