using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public GameObject loseMenu;

    private void Update()
    {

    }
    public void RestartGame()
    {
        // Reloads the scene in which the game is located
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void GoToMenu()
    {
        // Goes to main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        // Quits the game
        Application.Quit();
    }
}
