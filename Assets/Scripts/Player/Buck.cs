using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using Assets.Scripts.UI.Lose;

public class Buck : MonoBehaviour
{
    Level owner;
    public TextMeshPro currentPercentTxt;
    public float currentPercent;
    public Vector3 defaultLoc;
    public void Init(Level owner)
    {
        this.owner = owner;
        currentPercent = 0;
        currentPercentTxt.text = currentPercent + "%";

        defaultLoc = transform.localPosition;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.gameState != GameState.Gameplay)
        {
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer(Helper.COLOR_BALL_LAYER))
        {
            currentPercent += owner.percentPerBall;
            currentPercentTxt.text = Mathf.RoundToInt(currentPercent) + "%";
            transform.DOLocalMove(defaultLoc + Vector3.down * (currentPercent / 100f) * 2f, 1f);
            if (Mathf.RoundToInt(currentPercent) == 100)
            {
                transform.DOKill();
                GameManager.Instance.SetGameState(GameState.Win);
            }
        }
    }

    [Button()]
    public void Break()
    {
        transform.DOKill();

        transform.DOMove(GameManager.Instance.mainCam.transform.position, 1f)
                  .SetEase(Ease.Linear);

        transform.DOShakeRotation(1f)
            .OnComplete(()=> {
                ((LosePanel)UIManager.Instance.losePanel).loseType = ELoseType.BomBuck;

                GameManager.Instance.SetGameState(GameState.Lose);
            });


    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
