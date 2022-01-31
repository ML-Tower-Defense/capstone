using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class ObjectPooler : MonoBehaviour
{
    public List<ObjectPool> objectPools;
    public Dictionary<string, List<GameObject>> objectPoolDictionary;
    public static ObjectPooler SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPoolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (ObjectPool objectPool in objectPools)
        {
            List<GameObject> newObjectPool = new List<GameObject>();

            for (int i = 0; i < objectPool.size; i++)
            {
                GameObject obj = Instantiate(objectPool.prefab);
                obj.SetActive(false);
                newObjectPool.Add(obj);
            }

            objectPoolDictionary.Add(objectPool.tag, newObjectPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject(string tag)
    {
        List<GameObject> pooledObjects = objectPoolDictionary[tag];

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
