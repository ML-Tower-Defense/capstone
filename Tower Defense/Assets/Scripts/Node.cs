using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Node : MonoBehaviour
{
    public MoneyManager money;
    public GameObject tooPoorMessage;
    public GameObject singleMenu;
    public GameObject deleteTowerPrompt;

    private static GameObject whichNode;
    private GameObject childTower;

    private static int goldRefund;                 // Gold received from selling a tower

    AudioManager audioManager;

    void Start()
    {
        money = FindObjectOfType(typeof(MoneyManager)) as MoneyManager;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    public void OnMouseUpAsButton()
    {

        if (transform.childCount > 0 ) // Tower already on this tile
        {
            //print("Occupied!");

            if (BuildMenu.GameBuildSingle)
            {
                whichNode = transform.gameObject;
                openDeleteTowerMenu();
            }
            return;
        }

        // Quick build mode
        if (BuildMenu.GameInBuild)
        {
            whichNode = transform.gameObject;
            PlaceTower(0, whichNode);
        }

        // Single build mode
        if (BuildMenu.GameBuildSingle)
        {
            whichNode = transform.gameObject;
            singleMenu.SetActive(true);
            BuildMenu.GameBuildSingle = false;
        }
        return;
    }

    void PlaceTower(int whichTower, GameObject node)
    {
        int towerCost = 150;
        string towerName;
        int towerNum;
        if (whichTower == 0)
        {
            towerName = BuildMenu.towerName;
            towerNum = (towerName[towerName.Length - 1]) - '0';
        }
        else
        {
            towerNum = whichTower;
        }
        if (towerNum == 1) {
            towerCost = 150;
        }
        if (towerNum == 2) {
            towerCost = 200;
        }
        if (towerNum == 3) {
            towerCost = 300;
        }

        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild(towerNum);

        if (money.buy(towerCost))
        {
            audioManager.Play("BuySound");

            if (towerNum == 3)
            {
                childTower = Instantiate(towerToBuild, node.transform.position + new Vector3(0,0.5f,0), transform.rotation);
            }

            else
            {
                childTower = Instantiate(towerToBuild, node.transform.position, transform.rotation);
            }

            childTower.transform.parent = node.transform;
        }
        else
        {
            //Let player know they cannot place tower
            audioManager.Play("NoMoney");
            tooPoorMessage.SetActive(true);
        }
    }

    public void closePoorMessage()
    {
        audioManager.Play("ClickUI");
        tooPoorMessage.SetActive(false);
        BuildMenu.GameBuildSingle = true;
    }

    public void selectTower()
    {
        string towerName = EventSystem.current.currentSelectedGameObject.name;
        int towerNum = (towerName[towerName.Length - 1]) - '0';
        singleMenu.SetActive(false);
        BuildMenu.GameBuildSingle = true;
        PlaceTower(towerNum, whichNode);
    }

    public void deleteTower()
    {
        money.AddGold(goldRefund);

        try
        {
            Destroy(whichNode.transform.GetChild(0).gameObject);
        }
        catch (Exception)
        {
            ;
        }

        closeDeleteTowerMenu();
    }

    public void openDeleteTowerMenu()
    {
        //print("Open delete tower menu");
        BuildMenu.GameBuildSingle = false;

        switch (whichNode.transform.GetChild(0).gameObject.name)
        {
            case "DarkMageTower(Clone)":
                deleteTowerPrompt.GetComponentInChildren<TextMeshProUGUI>().text = "Do you want to sell tower for     40?";
                goldRefund = 40;
                break;

            case "ArcherTower(Clone)":
                deleteTowerPrompt.GetComponentInChildren<TextMeshProUGUI>().text = "Do you want to sell tower for     50?";
                goldRefund = 50;
                break;

            case "DragonTower(Clone)":
                deleteTowerPrompt.GetComponentInChildren<TextMeshProUGUI>().text = "Do you want to sell tower for     75?";
                goldRefund = 75;
                break;
        }

        deleteTowerPrompt.SetActive(true);
    }

    public void closeDeleteTowerMenu()
    {
        BuildMenu.GameBuildSingle = true;
        deleteTowerPrompt.SetActive(false);
    }
}
