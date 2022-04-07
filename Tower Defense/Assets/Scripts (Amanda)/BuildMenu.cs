using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildMenu : MonoBehaviour
{
    public static bool GameInBuild = false;
    public static bool GameBuildSingle = true;
    public GameObject buildMenuUI; //Singletons for multiple build menus?
    public GameObject buildText;
    public int towerNum;
    public static string towerName;
    public GameObject singleBuildMenu;

    //public MoneyManager money;

    public void OpenBuildMenu()
    {
        GameInBuild = false;
        buildMenuUI.SetActive(true);
        buildText.SetActive(false);
        GameBuildSingle = false;
    }

    public void CloseBuildMenu()
    {
        GameInBuild = false;
        GameBuildSingle = true;
        buildMenuUI.SetActive(false);
        buildText.SetActive(false);
        singleBuildMenu.SetActive(false);
    }

    public void BuildTower()
    {
        GameBuildSingle = false;
        towerName = EventSystem.current.currentSelectedGameObject.name;
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
}
