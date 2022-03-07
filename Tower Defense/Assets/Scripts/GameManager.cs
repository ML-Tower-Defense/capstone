using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject victoryMenu;
    public GameObject gameOverMenu;
    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true)
        {
            return;
        }
        else if (GateManager.gateCurrentHP <= 0)
        {
            GameOver();
        }
    }

    // Indicates that the player has won
    public void Victory()
    {
        isGameOver = true;
        victoryMenu.gameObject.SetActive(true);
    }

    // Indicates that the player has lost
    public void GameOver()
    {
        isGameOver = true;
        gameOverMenu.gameObject.SetActive(true);
    }

    // Reloads the current scene if the player wants to restart
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Redirects player back to the main menu
    public void QuitGame()
    {
        SceneManager.LoadScene("_MainMenu");
    }
}
