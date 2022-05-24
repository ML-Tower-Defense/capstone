using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    AudioManager audioManager;

    void Start () {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void floweryMeadow () {
        SceneManager.LoadScene("FloweryMeadow");
    }

    public void toonyPlains()
    {
        SceneManager.LoadScene("ToonyPlains");
    }

    public void closeGame() {
        Debug.Log("Exited Game");
        Application.Quit();
    }

    public void playClick() {
        audioManager.Play("Click");
    }
}
