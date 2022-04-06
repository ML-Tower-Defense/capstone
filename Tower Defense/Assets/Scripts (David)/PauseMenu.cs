using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Slider sliderM;
    public Slider sliderS;
    AudioManager audioManager;

    void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        sliderM.value = 1f;
        sliderS.value = .5f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        audioManager.Play("ClickUI");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause() {
        audioManager.Play("ClickUI");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void returnToMenu() {
        audioManager.Play("ClickUI");
        SceneManager.LoadScene("_MainMenu");
    }

    public void changeVolMusic() {
        audioManager.changeVolume(sliderM.value);
    }

    public void changeVolSFX() {
        audioManager.changeSFX(sliderS.value);
    }
}
