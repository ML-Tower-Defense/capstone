using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public static bool GameInBuild = false;
    public GameObject buildMenuUI;
    public GameObject buildText;
    //public MoneyManager money;

    public void OpenBuildMenu()
    {
        buildMenuUI.SetActive(true);
        //money = FindObjectOfType(typeof(MoneyManager)) as MoneyManager;
    }

    public void CloseBuildMenu()
    {
        GameInBuild = false;
        buildMenuUI.SetActive(false);
        buildText.SetActive(false);
    }

    public void BuildTower()
    {
        GameInBuild = true;
        buildMenuUI.SetActive(false);
        buildText.SetActive(true);
    }
}
