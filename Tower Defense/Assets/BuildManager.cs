using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private GameObject towerToBuild;
    public static BuildManager instance; //reference to this script, can only have one build manager
    public GameObject TowerPrefab; 
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more than one build manager in scene");
            return;
        }
        instance = this;
    }

    void Start()
    {
        towerToBuild = TowerPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetTowerToBuild()
    {
        return towerToBuild;
    }
}