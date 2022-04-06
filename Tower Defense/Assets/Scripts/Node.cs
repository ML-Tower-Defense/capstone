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
    GameObject towerBuilt;

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
        if (towerBuilt != null) //Tower already on this tile
        {
            Debug.Log("Tower found. Can't build here.");
            return;
        }
        PlaceTower();
    }

    void PlaceTower()
    {
        if (BuildMenu.GameInBuild)
        {
            int towerCost = 150;
            string towerName = BuildMenu.towerName;
            int towerNum = (towerName[towerName.Length - 1]) - '0';
            Debug.Log(towerNum);
            GameObject towerToBuild = BuildManager.instance.GetTowerToBuild(towerNum);
            if (money.buy(towerCost))
            {
                //GameObject towerToBuild = BuildManager.instance.GetTowerToBuild(1);

                towerBuilt = Instantiate(towerToBuild, transform.position, transform.rotation);

            }
            else
            {
                //Let player know they cannot place tower
                tooPoorMessage.SetActive(true);
            }
        }
    }
    
    public void closePoorMessage()
    {
        tooPoorMessage.SetActive(false);
    }
}
