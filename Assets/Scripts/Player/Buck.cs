using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using Assets.Scripts.UI.Lose;
using Assets.Scripts.UI.Play;
using Assets.Scripts.UI.Puzzle;
using System;
using Random = UnityEngine.Random;
using Assets.Scripts.Player;

public class Buck : MonoBehaviour
{
    Level owner;
    public TextMeshPro currentPercentTxt;
    public float currentPercent;
    public Vector3 defaultLoc;
    public Transform piece;
    public Transform visual;
    private bool hasPiece;
    private bool complete;

    public static Action OnVisualUpdate;

    [FoldoutGroup("Visual"), SerializeField]
    protected MeshFilter colliderMesh;
    [FoldoutGroup("Visual"), SerializeField]
    protected MeshCollider coll;
    [FoldoutGroup("Visual"), SerializeField]
    protected MeshFilter headMesh;
    [FoldoutGroup("Visual"), SerializeField]
    protected MeshFilter bodyMesh;
    [FoldoutGroup("Visual"), SerializeField]
    protected MeshRenderer headMaterial;
    [FoldoutGroup("Visual"), SerializeField]
    protected MeshRenderer bodyMaterial;

    public Dragon dragon;
    public void Init(Level owner, bool hasPiece)
    {
        this.owner = owner;
        currentPercent = 0;
        currentPercentTxt.text = currentPercent + "%";

        defaultLoc = visual.transform.localPosition;
        complete = false;
        this.hasPiece = hasPiece;
        piece.gameObject.SetActive(hasPiece);
        OnThemeChange();
        dragon.SetTrigger();
        OnVisualUpdate += OnThemeChange;
    }

    private void OnThemeChange()
    {
        dragon.gameObject.SetActive(GameManager.Instance.currentTheme.id == "Theme0");

        //colliderMesh.mesh = GameManager.Instance.currentTheme.colliderBuck;
        coll.sharedMesh = GameManager.Instance.currentTheme.colliderBuck;
        headMesh.mesh = GameManager.Instance.currentTheme.headBuck;
        bodyMesh.mesh = GameManager.Instance.currentTheme.bodyBuck;

        headMaterial.material = GameManager.Instance.currentTheme.headBuckMaterial;
        bodyMaterial.material = GameManager.Instance.currentTheme.bodyBuckMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (complete)
        {
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer(Helper.COLOR_BALL_LAYER))
        {
            //currentPercent += owner.percentPerBall;
            currentPercent += 1;
            currentPercentTxt.text = Mathf.RoundToInt((currentPercent / owner.amountBall) * 100f) + "%";
            visual.transform.DOLocalMove(defaultLoc + Vector3.down * (currentPercent / owner.amountBall) * 2f, 1f);
            if (currentPercent == owner.amountBall)
            {
                visual.transform.DOKill();
                dragon.SetTrigger("Eat");
                CollectPiece();
            }
        }
    }
    private void CollectPiece()
    {
        if (hasPiece && GameManager.Instance.AnyPiecePuzzle())
        {
            piece.DOScale(Vector3.one * 1.5f, 0.25f)
                   .OnComplete(() =>
                   {
                       piece.DOMove(((PlayPanel)UIManager.Instance.gamePlayPanel).puzzleBtn.transform.position, 1f)
                       .SetEase(Ease.Linear)
                            .OnComplete(() =>
                            {
                                if (GameManager.Instance.currentGameState == GameState.NormalMode)
                                    UIManager.Instance.newPuzzlePiecePanel.Open();
                                GameManager.Instance.SetGameState(GameState.Win, 0);

                            });
                   });
        }
        else
            GameManager.Instance.SetGameState(GameState.Win);

        DataManager.Instance.GetData().AddDailyMissionValue(EDailyMissionID.CollectBalls, owner.amountBall);
        DataManager.Instance.GetData().AddDailyMissionValue(EDailyMissionID.CompleteLevel, 1);

        complete = true;
    }

    [Button()]
    public void Break()
    {
        currentPercentTxt.gameObject.SetActive(false);
        piece.gameObject.SetActive(false);
        visual.transform.DOKill();


        visual.transform.DOMove(Random.insideUnitCircle.normalized * 15f + (Vector2)transform.position, 1f)
                  .SetEase(Ease.Linear);


        visual.transform.DORotate(Random.insideUnitSphere.normalized * 720f, 1f, RotateMode.FastBeyond360);
        ((LosePanel)UIManager.Instance.losePanel).loseType = ELoseType.BomBuck;

        //if (GameManager.Instance.currentGameState == GameState.NormalMode)
        GameManager.Instance.SetGameState(GameState.Lose);

    }
    private void OnDestroy()
    {
        visual.transform.DOKill();
        OnVisualUpdate -= OnThemeChange;

    }
}
