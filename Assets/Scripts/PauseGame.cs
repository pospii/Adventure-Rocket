using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
                Pause();
        }
        if (Input.GetKey(KeyCode.O))
        {
                Resume();
        }
    }
    void Pause()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
