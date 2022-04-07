using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public MoneyManager money;
    public GameObject tooPoorMessage;
    private int towerCost;
    private GameObject towerBuilt;
    public GameObject menu;

    void Start()
    {
        money = FindObjectOfType(typeof(MoneyManager)) as MoneyManager;
    }

    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }*/

    void OnMouseDown()
    {
        if (towerBuilt != null) // Tower already on this tile
        {
            Debug.Log("Tower found. Can't build here.");
            return;
        }
        if (!BuildMenu.GameInBuild)
        {
            //menu.SetActive(true);
            Debug.Log("This is when build menu opens again");
            //PlaceTower();
        }
        else
        {
            PlaceTower();
        }
        
    }

    void PlaceTower()
    {
        int towerCost = 150;
        string towerName = BuildMenu.towerName;
        int towerNum = (towerName[towerName.Length - 1]) - '0';
        Debug.Log(towerNum);
        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild(towerNum);
        if (money.buy(towerCost))
        {
            towerBuilt = Instantiate(towerToBuild, transform.position, transform.rotation);
        }
        else
        {
            //Let player know they cannot place tower
            tooPoorMessage.SetActive(true);
        }
    }

    public void closePoorMessage()
    {
        tooPoorMessage.SetActive(false);
    }
}
