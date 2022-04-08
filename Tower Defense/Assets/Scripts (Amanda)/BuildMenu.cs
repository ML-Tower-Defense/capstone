using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildMenu : MonoBehaviour
{
    public static bool GameInBuild = false;
    public GameObject buildMenuUI;
    public GameObject buildText;
    public int towerNum;
    public static string towerName;
    AudioManager audioManager;

    void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    //public MoneyManager money;

    public void OpenBuildMenu()
    {
        audioManager.Play("ClickUI");
        buildMenuUI.SetActive(true);
    }

    public void CloseBuildMenu()
    {
        GameInBuild = false;
        audioManager.Play("ClickUI");
        buildMenuUI.SetActive(false);
        buildText.SetActive(false);
    }

    public void BuildTower()
    {
        towerName = EventSystem.current.currentSelectedGameObject.name;
        audioManager.Play("ClickUI");
        GameInBuild = true;
        buildMenuUI.SetActive(false);
        buildText.SetActive(true);
    }

}
