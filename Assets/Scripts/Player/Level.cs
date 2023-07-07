using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class Level : MonoBehaviour
{
    public float cameraSize = 65;

    public bool hasTut;

    public float percentPerBall;
    public Buck buck;
    public List<Pin> pins;
    public int amountBall;

    [SerializeField]
    protected GameObject handTut;
    [ShowIf("hasTut"), SerializeField]
    protected List<Pin> pinsTut;
    public virtual void Init(bool hasPiece)
    {
        buck.Init(this, hasPiece && DataManager.Instance.CurrentStage != 0 && DataManager.Instance.CurrentStage % 6 == 0);

        InitPins();

        if (hasTut && DataManager.Instance.CurrentStage < 4)
            StartCoroutine(IETut());
    }
    protected virtual void InitPins()
    {
        int length = pins.Count;
        for (int i = 0; i < length; i++)
        {
            pins[i].Init();
        }
    }
    protected IEnumerator IETut()
    {
        int length = pinsTut.Count;
        for (int i = 0; i < length; i++)
        {
            handTut.SetActive(true);
            handTut.transform.position = pinsTut[i].transform.position - pinsTut[i].transform.up * 5;
            handTut.transform.DOMove(pinsTut[i].transform.position, 1f)
                            .SetLoops(-1, LoopType.Restart);
            yield return new WaitUntil(() => !pinsTut[i].canTouch);
            handTut.transform.DOKill();
            handTut.SetActive(false);
        }
    }

    protected virtual void OnDestroy()
    {
        handTut.transform.DOKill();
    }

#if UNITY_EDITOR
    [SerializeField]
    protected Ball ballPrefabs;
    [SerializeField]
    protected BoxCollider2D spawnLoc;
    [SerializeField]
    protected Transform ballParent;
    [SerializeField, MinMaxSlider(0.5f, 5f)]
    protected Vector2 size = new Vector2(1,1);

    [Button()]
    public virtual void SpawnBall(int amount, EBallType type)
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
    public virtual void DeleteAllBall()
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
