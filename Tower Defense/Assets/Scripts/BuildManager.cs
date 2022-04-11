using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private GameObject towerToBuild;
    public static BuildManager instance; //reference to this script, can only have one build manager
    public GameObject TowerPrefab1;
    public GameObject TowerPrefab2;
    public GameObject TowerPrefab3;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There can only be one build manager in scene!");
            return;
        }
        instance = this;
    }

    void Start()
    {
        //towerToBuild = TowerPrefab;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetTowerToBuild(int towerNum)
    {
        if (towerNum == 1)
            towerToBuild = TowerPrefab1;
        else if (towerNum == 2)
            towerToBuild = TowerPrefab2;
        else
            towerToBuild = TowerPrefab3;

        return towerToBuild;
    }
}
