using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float percentPerBall;
    public Buck buck;
    public List<Pin> pins;
    private void Start()
    {
        Init();
    }
    public virtual void Init()
    {
        buck.Init(this);

        int length = pins.Count;
        for (int i = 0; i < length; i++)
        {
            pins[i].Init();
        }
    }

#if UNITY_EDITOR
    [SerializeField]
    private Ball ballPrefabs;
    [SerializeField]
    private BoxCollider2D spawnLoc;
    [SerializeField]
    private int amountBall;
    [SerializeField]
    private Transform ballParent;
    [SerializeField,MinMaxSlider(0.5f, 1f)]
    private Vector2 size;

    [Button()]
    public void SpawnBall(int amount, EBallType type)
    {
        amountBall += amount;
        for (int i = 0; i < amount; i++)
        {
            Ball ball = PrefabUtility.InstantiatePrefab(ballPrefabs) as Ball;
            ball.transform.position = new Vector3(Random.Range(spawnLoc.bounds.min.x, spawnLoc.bounds.max.x), Random.Range(spawnLoc.bounds.min.y, spawnLoc.bounds.max.y), 0);
            ball.transform.parent = ballParent;
            ball.Init(type, Random.Range(size.x, size.y));
        }
        UpdatePercent();
    }
    [Button()]
    public void DeleteAllBall()
    {
        while (ballParent.childCount != 0)
        {
            DestroyImmediate(ballParent.GetChild(0).gameObject);
        }
        amountBall = 0;
    }
    public void UpdatePercent()
    {
        percentPerBall = 100f / amountBall;
    }
#endif
}
