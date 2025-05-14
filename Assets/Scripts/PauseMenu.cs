using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; 
    GameController gameController; 

    public GameObject pauseMenuUI; 

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameIsPaused = true; 
            if (GameIsPaused) 
            {
                pauseMenuUI.SetActive(true); 
                gameController.PauseGame(); 
            }
        }
    }
}
