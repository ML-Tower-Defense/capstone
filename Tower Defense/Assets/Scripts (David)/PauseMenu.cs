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
    public BuildMenu buildMenu;
    AudioManager audioManager;

    void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        sliderM.value = .3f;
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
        BuildMenu.GameBuildSingle = true;
        buildMenu.ActivateBuild();
        audioManager.Play("ClickUI");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause() {
        buildMenu.DeactivateBuild();
        BuildMenu.GameBuildSingle = false;
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
