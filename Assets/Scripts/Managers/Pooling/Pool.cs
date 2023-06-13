using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using System;

[System.Serializable]
public class Pool
{
    public int size;
    [SerializeField]
    public PoolingObject prefab;

    public Pool(int size, PoolingObject prefab)
    {
        this.size = size;
        this.prefab = prefab;
    }
}