using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void PlayGame () {
        SceneManager.LoadScene("Map1");
    }

    public void closeGame() {
        Debug.Log("Exited Game");
        Application.Quit();
    }
}
