using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    // Pool struct
    [System.Serializable]
    public struct Pool
    {
        [Tooltip("Object that will be created on startup")]
        public GameObject objectToPool;
        [Tooltip("The amount of those objects to be created")]
        public int amountToPool;
        [HideInInspector] public List<GameObject> objectPool;

        public Pool(GameObject _objectToPool, int _amountToPool, List<GameObject> _objectPool)
        {
            objectToPool = _objectToPool;
            amountToPool = _amountToPool;
            objectPool = _objectPool;
        }
    }

    [Header("Object Pools")]
    [SerializeField] List<Pool> pools = new List<Pool>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        FillPools();
    }

    /// <summary>
    /// Fill all predetermined pools
    /// </summary>
    private void FillPools()
    {
        foreach (Pool pool in pools)
        {
            FillPool(pool.objectToPool, pool.amountToPool, pool.objectPool);
        }
    }

    /// <summary>
    /// Fill an indivdual pool
    /// </summary>
    private void FillPool(GameObject _objectToPool, int _amountToPool, List<GameObject> _objectPool)
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject obj = Instantiate(_objectToPool);
            obj.name = _objectToPool.name;
            obj.SetActive(false);
            _objectPool.Add(obj);
        }
    }

    /// <summary>
    /// request an object from the pool by type
    /// </summary>
    public GameObject Get(GameObject objectType)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].objectToPool == objectType)
            {
                return GetObjectFromPool(pools[i]);
            }
        }
        return null;
    }

    /// <summary>
    /// If an object exists within a specified pool that isnt already active, return the object
    /// </summary>
    private GameObject GetObjectFromPool(Pool pool)
    {
        for (int i = 0; i < pool.objectPool.Count; i++)
        {
            if (!pool.objectPool[i].activeInHierarchy)
            {
                return pool.objectPool[i];
            }
        }

        ExtendPool(pool, 5);
        return GetObjectFromPool(pool);
    }

    /// <summary>
    /// adds more objects to pool
    /// </summary>
    private void ExtendPool(Pool pool, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(pool.objectToPool);
            obj.name = pool.objectToPool.name;
            obj.SetActive(false);
            pool.objectPool.Add(obj);
        }
    }

    /// <summary>
    /// Deactivates all objects in each pool
    /// </summary>
    public void ReleaseAllPools()
    {
        foreach (Pool pool in pools)
        {
            foreach(GameObject obj in pool.objectPool)
            {
                obj.SetActive(false);
            }
        }
    }
}
