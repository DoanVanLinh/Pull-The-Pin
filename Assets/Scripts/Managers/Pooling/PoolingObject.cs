using System.Collections;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
public abstract class PoolingObject : SerializedMonoBehaviour
{
    [FoldoutGroup("Base")]
    public string nameObj;
    [FoldoutGroup("Base")]
    public Action<PoolingObject> OnDispose;
    public virtual void Start()
    {
        gameObject.SetActive(false);
    }
    public abstract void Init();
    public virtual void Dispose()
    {
        OnDispose?.Invoke(this);
        gameObject.SetActive(false);
    }
}