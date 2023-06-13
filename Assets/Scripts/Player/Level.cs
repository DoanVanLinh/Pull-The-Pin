using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float percentPerBall;
    public Buck buck;
    public List<Pin> pins;

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
    private Transform spawnLoc;
    [SerializeField]
    private int amountBall;
    [Button()]
    public void SpawnBall(int amount, BallType type)
    {
        amountBall += amount;
        for (int i = 0; i < amount; i++)
        {
            Ball ball = Instantiate(ballPrefabs, (Vector2)spawnLoc.position + Random.insideUnitCircle.normalized * 1f, Quaternion.identity);
            ball.Init(type);
        }
        UpdatePercent();
    }


    public void UpdatePercent()
    {
        percentPerBall = 100 / amountBall;
    }
#endif
}
