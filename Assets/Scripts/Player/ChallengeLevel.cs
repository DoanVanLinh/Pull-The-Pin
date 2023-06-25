using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class ChallengeLevel : Level
{
    public static Action onBallChange;
    public string id;
    public int limitedMove;
    public List<Ball> balls;
    public int CurrentMove
    {
        get => currentMove;
        set
        {
            currentMove = value;
        }
    }

    private int currentMove;
    public override void Init(bool hasPiece)
    {
        hasPiece = false;
        base.Init(hasPiece);
        CurrentMove = limitedMove;
        FindHeightestBall();
        onBallChange += FindHeightestBall;
    }

    protected override void InitPins()
    {
        int length = pins.Count;
        for (int i = 0; i < length; i++)
        {
            ((ChallengePin)pins[i]).Init(this);
        }
    }

    public void FindHeightestBall()
    {
        Vector2 maxPosition = new Vector2();
        for (int i = 0; i < amountBall; i++)
        {
            if (balls[i].transform.position.y > maxPosition.y)
            {
                maxPosition = balls[i].transform.position;
                Ball.hieghtestBall = balls[i];
            }
        }
        GameManager.Instance.UpdateCamChallegeLoc(10);
        
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onBallChange -= FindHeightestBall;
    }
#if UNITY_EDITOR

    public override void SpawnBall(int amount, EBallType type)
    {
        amountBall += amount;
        for (int i = 0; i < amount; i++)
        {
            Ball ball = PrefabUtility.InstantiatePrefab(ballPrefabs) as Ball;
            ball.transform.position = new Vector3(Random.Range(spawnLoc.bounds.min.x, spawnLoc.bounds.max.x), Random.Range(spawnLoc.bounds.min.y, spawnLoc.bounds.max.y), 0);
            ball.transform.parent = ballParent;
            ball.Init(type, Random.Range(size.x, size.y));
            balls.Add(ball);
        }
        UpdatePercent();
    }
    public override void DeleteAllBall()
    {
        base.DeleteAllBall();
        balls.Clear();
    }
#endif
}
