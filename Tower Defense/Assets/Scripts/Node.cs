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
            Debug.Log("can't build here");
            return;
        }
        PlaceTower();
    }

    void PlaceTower()
    {
        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();
        tower = (GameObject)Instantiate(towerToBuild, transform.position, transform.rotation);

    }
}
