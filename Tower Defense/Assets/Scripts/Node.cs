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
    AudioManager audioManager;

    void Start()
    {
        money = FindObjectOfType(typeof(MoneyManager)) as MoneyManager;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
        PlaceTower();
    }

    void PlaceTower()
    {
        if (BuildMenu.GameInBuild)
        {
            towerCost = 150;

            string towerName = BuildMenu.towerName;
            int towerNum = (towerName[towerName.Length - 1]) - '0';

            GameObject towerToBuild = BuildManager.instance.GetTowerToBuild(towerNum);

            if (money.buy(towerCost))
            {
                // GameObject towerToBuild = BuildManager.instance.GetTowerToBuild(1);
                audioManager.Play("BuySound");
                towerBuilt = Instantiate(towerToBuild, transform.position, transform.rotation);

            }
            else
            {
                // Let player know they cannot place tower
                audioManager.Play("NoMoney");
                tooPoorMessage.SetActive(true);
            }
        }
    }

    public void closePoorMessage()
    {
        audioManager.Play("ClickUI");
        tooPoorMessage.SetActive(false);
    }
}
