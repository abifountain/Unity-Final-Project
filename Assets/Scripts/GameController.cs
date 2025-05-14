using UnityEngine;
using System; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.SceneManagement; 

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen; 
    public GameObject pauseMenuUI; 

    void Start()
    {
        PlayerHealth.OnPlayerDied += GameOverScreen; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pauseMenuUI.SetActive(true); 
            PauseGame(); 
        }
    }

    void GameOverScreen()
    {
        MusicManager.PauseBackgroundMusic(); 
        gameOverScreen.SetActive(true); 
        PauseGame(); 
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; 
    }

    public void ResumeGame() 
    {
        Time.timeScale = 1f; 
        pauseMenuUI.SetActive(false); 
    }

    public void ResetGame()
    {
        gameOverScreen.SetActive(false); 
        MusicManager.PlayBackgroundMusic(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        ResumeGame(); 
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
