using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool isGameOver;
    public GameObject gameOverScreen;

    public static Vector2 lastCheckPointPos = new Vector2(-18, -10);
    private void Awake()
    {
        isGameOver = false;
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGameManager();
            
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ResumeGame();
        }
        if(isGameOver)
        {
            Invoke("ShowGameOverScreen", 2f);
        }
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            ReplayLevel();
        }
    }
    void ShowGameOverScreen()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
    public void PauseGameManager()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);


    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ReplayLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
