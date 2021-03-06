using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildMenu : MonoBehaviour
{
    public static bool GameInBuild = false;
    public static bool GameBuildSingle = true;
    public GameObject buildMenuUI; 
    public GameObject buildText;
    public int towerNum;
    public static string towerName;
    public GameObject singleBuildMenu;
    AudioManager audioManager;
    public GameObject buildButton;

    void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void OpenBuildMenu()
    {
        GameInBuild = false;
        audioManager.Play("ClickUI");
        buildMenuUI.SetActive(true);
        buildText.SetActive(false);
        GameBuildSingle = false;
    }

    public void CloseBuildMenu()
    {
        GameInBuild = false;
        GameBuildSingle = true;
        audioManager.Play("ClickUI");
        buildMenuUI.SetActive(false);
        buildText.SetActive(false);
        singleBuildMenu.SetActive(false);
    }

    public void BuildTower()
    {
        GameBuildSingle = false;
        towerName = EventSystem.current.currentSelectedGameObject.name;
        audioManager.Play("ClickUI");
        GameInBuild = true;
        buildMenuUI.SetActive(false);
        buildText.SetActive(true);
        singleBuildMenu.SetActive(false);
    }
    
    public void BuildOneTower()
    {
        towerName = EventSystem.current.currentSelectedGameObject.name;
        CloseBuildMenu();
    }

    public void DeactivateBuild()
    {
        buildButton.SetActive(false);
        this.CloseBuildMenu();
    }

    public void ActivateBuild()
    {
        buildButton.SetActive(true);
    }
}
