using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    public GameObject[] towerSpots;

    private int maxTowerCount = 5;
    private int currentTowerCount = 0;
    private string towerTag = "Tower";
    private EnemyAgent agent;
    private Dictionary<int, int> occupiedNodes;

    // Start is called before the first frame update
    void Start()
    {
        occupiedNodes = new Dictionary<int, int>();
        agent = GameObject.Find("GolemEnemy").GetComponent<EnemyAgent>();
        InitializeEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);

        if (currentTowerCount == 5 && towers.Length == 0)
        {
            EndEnvironment();
        }
    }

    private void InitializeEnvironment()
    {
        while (currentTowerCount < maxTowerCount)
        {
            int towerType = Random.Range(0, towerPrefabs.Length);
            int towerSpot = Random.Range(0, towerSpots.Length);

            if (!occupiedNodes.ContainsKey(towerSpot))
            {
                Node node = towerSpots[towerSpot].GetComponent<Node>();
                
                if (towerType == 2)
                {
                    Instantiate(towerPrefabs[towerType], node.transform.position + new Vector3(0, 0.5f, 0), node.transform.rotation);
                }
                else
                {
                    Instantiate(towerPrefabs[towerType], node.transform.position, node.transform.rotation);
                }

                occupiedNodes.Add(towerSpot, towerType);

                currentTowerCount++;
            }
        }
    }

    private void EndEnvironment()
    {
        currentTowerCount = 0;
        occupiedNodes.Clear();
        agent.EndEpisode();
    }
}
