using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildMenu : MonoBehaviour
{
    public static bool GameInBuild = false;
    public GameObject buildMenuUI;
    public GameObject buildText;

    public void OpenBuildMenu()
    {
        Debug.Log("OpenBuildMenu");
        buildMenuUI.SetActive(true);
    }

    public void CloseBuildMenu()
    {
        Debug.Log("CloseBuildMenu");
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
