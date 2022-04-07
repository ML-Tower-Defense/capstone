using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildMenu : MonoBehaviour
{
    public static bool GameInBuild = false;
    public GameObject buildMenuUI; //Singletons for multiple build menus?
    public GameObject buildText;
    public int towerNum;
    public static string towerName;

    //public MoneyManager money;

    public void OpenBuildMenu()
    {
        GameInBuild = false;
        buildMenuUI.SetActive(true);
        buildText.SetActive(false);
        
    }

    public void CloseBuildMenu()
    {
        GameInBuild = false;
        buildMenuUI.SetActive(false);
        buildText.SetActive(false);
    }

    public void BuildTower()
    {
        towerName = EventSystem.current.currentSelectedGameObject.name;
        GameInBuild = true;
        buildMenuUI.SetActive(false);
        buildText.SetActive(true);
    }

    public void BuildOneTower()
    {
        towerName = EventSystem.current.currentSelectedGameObject.name;
        GameInBuild = true;
        buildMenuUI.SetActive(false);
    }
}
