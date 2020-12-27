using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public static bool isGamePaused;
    public GameObject pausePanel;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isGamePaused) Resume();
            else Pause();
        }
    }
    public void Resume() {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    void Pause() {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    public void BackToMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
    public  void ExitButton( ) {
        Application.Quit();
    }
}
