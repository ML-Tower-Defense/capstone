using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public MoneyManager money;
    public GameObject tooPoorMessage;
    public GameObject singleMenu;

    private static GameObject whichNode;

    AudioManager audioManager;

    void Start()
    {
        money = FindObjectOfType(typeof(MoneyManager)) as MoneyManager;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void OnMouseUpAsButton()
    {
        if (transform.childCount > 0) // Tower already on this tile
        {
            print("Occupied! Can't build here.");
            return;
        }

        // Quick build mode
        if (BuildMenu.GameInBuild)
            PlaceTower(0, gameObject);

        // Single build mode
        if (BuildMenu.GameBuildSingle)
        {
            singleMenu.SetActive(true);
            BuildMenu.GameBuildSingle = false;
            whichNode = gameObject;
        }
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

        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild(towerNum);

        if (money.buy(towerCost))
        {
            audioManager.Play("BuySound");

            GameObject childTower = Instantiate(towerToBuild, node.transform.position, transform.rotation) as GameObject;
            childTower.transform.parent = node.transform;

            print("Built tower! Now occupied.");
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
}
