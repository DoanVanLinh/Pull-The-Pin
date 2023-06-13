using System.Collections;
using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class CommonTextPopup : PoolingObject
{
    public Vector2 newPosition;
    public float timeAnimation;

    public TextMeshPro content;
    public List<TextPopup> listTypeText;
    //public Canvas canvas;

    public override void Start()
    {
        base.Start();
        //canvas.worldCamera = Camera.main;
    }
    public override void Init()
    {

    }
    public void LoadText(string text, TypeText type)
    {
        content.transform.DOLocalMove(newPosition, timeAnimation).SetEase(Ease.InOutSine).OnComplete(() =>
            content.transform.DOLocalMove(Vector2.zero, timeAnimation).SetEase(Ease.InOutSine).OnComplete(() =>
                Dispose()
            )
        );

        if (Helper.IsOutOfView(transform.position))
        {
            Dispose();
            return;
        }

        if (!content.text.Equals(text))
            content.text = text;

        for (int i = 0; i < listTypeText.Count; i++)
        {
            if (listTypeText[i].typeText == type)
            {
                content.color = listTypeText[i].color;
                transform.localScale = Vector3.one * listTypeText[i].size;
                break;
            }
        }

    }
    public void OnAniDone()
    {
        Dispose();
    }

    public override void Dispose()
    {
        content.transform.DOKill();
        base.Dispose();
    }
}
[Serializable]
public class TextPopup
{
    public TypeText typeText;
    public Color color;
    public float size;
}