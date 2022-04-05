using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject tower;
    public MoneyManager money;
    public GameObject tooPoorMessage;

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
        if (tower != null) //Tower already on this tile
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
            int towerCost = 150; //eventually dynamically grab price of tower
            if (money.buy(towerCost))
            {
                GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();

                tower = Instantiate(towerToBuild, transform.position, transform.rotation);
            }
            else
            {
                //Let player know they cannot place tower
                tooPoorMessage.SetActive(true);
            }
        }
    }
}
