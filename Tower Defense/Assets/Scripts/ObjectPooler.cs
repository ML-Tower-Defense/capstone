using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    public string objectTag;        // Sets the tag that corresponds with the object (ex: "enemy")
    public GameObject prefab;       // Sets the game object that the pool will handle
    public int size;                // Sets the size of the pool
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<ObjectPool> objectPools;        
    public Dictionary<string, List<GameObject>> objectPoolDictionary;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPoolDictionary = new Dictionary<string, List<GameObject>>();

        InstantiatePooledObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Preinstantiates game objects before runtime for every object pool that was set
    public void InstantiatePooledObjects()
    {
        foreach (ObjectPool objectPool in objectPools)
        {
            List<GameObject> newObjectPool = new List<GameObject>();

            for (int i = 0; i < objectPool.size; i++)
            {
                GameObject obj = Instantiate(objectPool.prefab);
                obj.SetActive(false);
                newObjectPool.Add(obj);
            }

            objectPoolDictionary.Add(objectPool.objectTag, newObjectPool);
        }
    }

    // Selects an object pool based on the object tag that was passed in and returns an object from
    // the pool that is currently not being used
    public GameObject GetPooledObject(string objectTag)
    {
        List<GameObject> pooledObjects = objectPoolDictionary[objectTag];

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
