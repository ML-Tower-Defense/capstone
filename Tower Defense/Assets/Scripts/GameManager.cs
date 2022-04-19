using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject victoryMenu;
    public GameObject gameOverMenu;
    private BuildMenu buildMenu;
    AudioManager audioManager;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        buildMenu = GetComponent<BuildMenu>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
        BuildMenu.GameBuildSingle = false;
        buildMenu.CloseBuildMenu();
        isGameOver = true;
        victoryMenu.gameObject.SetActive(true);
        audioManager.StopAllAudio();
        audioManager.Play("WinGame");
    }

    // Indicates that the player has lost
    public void GameOver()
    {
        BuildMenu.GameBuildSingle = false;
        buildMenu.CloseBuildMenu();
        isGameOver = true;
        gameOverMenu.gameObject.SetActive(true);
        audioManager.StopAllAudio();
        audioManager.Play("LoseGame");
    }

    // Reloads the current scene if the player wants to restart
    public void RestartGame()
    {
        audioManager.Play("ClickUI");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Redirects player back to the main menu
    public void QuitGame()
    {
        audioManager.Play("ClickUI");
        SceneManager.LoadScene("_MainMenu");
    }

}
