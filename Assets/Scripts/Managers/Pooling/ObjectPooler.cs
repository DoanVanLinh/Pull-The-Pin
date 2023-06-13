using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;
//using Assets.Scripts.Enemy;

public class ObjectPooler : SerializedMonoBehaviour
{
    #region Singleton
    public static ObjectPooler Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        SpawnPool();


    }
    #endregion

    public List<Pool> poolers = new List<Pool>();
    [ShowInInspector]
    public Dictionary<string, Queue<PoolingObject>> poolingCanActives = new Dictionary<string, Queue<PoolingObject>>();
    [ShowInInspector]
    public Dictionary<string, Queue<PoolingObject>> poolingsCanDispose = new Dictionary<string, Queue<PoolingObject>>();

    public void SpawnPool()
    {
        foreach (Pool pooler in poolers)
        {
            Queue<PoolingObject> pooling = new Queue<PoolingObject>();
            for (int i = 0; i < pooler.size; i++)
            {
                pooling.Enqueue(Instantiate(pooler.prefab, transform));
            }
            poolingCanActives.Add(pooler.prefab.nameObj, pooling);
            poolingsCanDispose.Add(pooler.prefab.nameObj, new Queue<PoolingObject>());
        }
    }

    public PoolingObject Spawn(string nameObj, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Vector3 localScale = default(Vector3))
    {
        if (!poolingCanActives.ContainsKey(nameObj))
        {
            Debug.LogWarning("Not found " + nameObj);
            return null;
        }

        if (poolingCanActives[nameObj].Count == 0)
            return null;

        PoolingObject spawnObj = poolingCanActives[nameObj].Dequeue();

        spawnObj.transform.position = position;
        spawnObj.transform.rotation = rotation;
        spawnObj.transform.localScale = localScale == default(Vector3) ? Vector3.one : localScale;
        spawnObj.gameObject.SetActive(true);
        spawnObj.Init();
        spawnObj.OnDispose += OnPoolingObjectDispose;

        poolingsCanDispose[nameObj].Enqueue(spawnObj);

        return spawnObj;
    }
    public PoolingObject SpawnContainName(string nameObj, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion))
    {
        List<string> aviableKeys = poolingCanActives.Keys.Where(k => k.Contains(nameObj) && poolingCanActives[k].Count != 0).ToList();

        if (aviableKeys.Count == 0)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Not found any obj has name: " + nameObj);
#endif
            return null;
        }

        string key = aviableKeys.OrderBy(o => Random.Range(-1f, 1f)).FirstOrDefault();

        PoolingObject spawnObj = poolingCanActives[key].Dequeue();

        spawnObj.transform.position = position;
        spawnObj.transform.rotation = rotation;
        spawnObj.gameObject.SetActive(true);
        spawnObj.Init();
        spawnObj.OnDispose += OnPoolingObjectDispose;

        poolingsCanDispose[key].Enqueue(spawnObj);

        return spawnObj;
    }

    private void OnPoolingObjectDispose(PoolingObject poolingObj)
    {
        poolingObj.OnDispose -= OnPoolingObjectDispose;
        poolingCanActives[poolingObj.nameObj].Enqueue(poolingObj);
        if (poolingsCanDispose[poolingObj.nameObj].Count != 0)
            poolingsCanDispose[poolingObj.nameObj].Dequeue();
    }

    /// <summary>
    /// By name
    /// </summary>
    /// <param name="nameObj"> name of obj</param>
    public void ForceDispose(string nameObj)
    {
        int length = poolingsCanDispose[nameObj].Count;
        for (int i = 0; i < length; i++)
        {
            PoolingObject tempPoolingObj = poolingsCanDispose[nameObj].Dequeue();
            tempPoolingObj.OnDispose -= OnPoolingObjectDispose;

            tempPoolingObj.Dispose();
            poolingCanActives[nameObj].Enqueue(tempPoolingObj);
        }
    }
    /// <summary>
    /// By Objct
    /// </summary>
    /// <param name="poolingObj"> object </param>
    public void ForceDispose(PoolingObject poolingObj = null)
    {
        PoolingObject tempPoolingObj = poolingsCanDispose[poolingObj.nameObj].Dequeue();
        tempPoolingObj.OnDispose -= OnPoolingObjectDispose;

        tempPoolingObj.Dispose();
        poolingCanActives[poolingObj.nameObj].Enqueue(tempPoolingObj);
    }
    /// <summary>
    /// Forece Dispose all
    /// </summary>
    public void ForceDispose()
    {
        foreach (var item in poolingsCanDispose)
        {
            while (item.Value.Count != 0)
            {
                //if (item.Key == "BossChest") continue;

                PoolingObject tempPoolingObj = poolingsCanDispose[item.Key].Dequeue();
                tempPoolingObj.OnDispose -= OnPoolingObjectDispose;
                if (item.Key != "BossChest")
                {
                    tempPoolingObj.Dispose();
                }
                if (!poolingCanActives[item.Key].Contains(tempPoolingObj))
                    poolingCanActives[item.Key].Enqueue(tempPoolingObj);
            }
        }
    }
}