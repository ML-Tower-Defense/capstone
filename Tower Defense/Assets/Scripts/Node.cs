using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject tower;


    /*void Start()
    {

    }

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
            GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();
            //string towerName = towerToBuild.transform.name;

            tower = Instantiate(towerToBuild, transform.position, transform.rotation);

            //if (towerName != "Tower")
            //{
            //    tower.AddComponent<Tower>();
            //}

            //tower.AddComponent(Type.GetType(towerName));
        }
    }
}
