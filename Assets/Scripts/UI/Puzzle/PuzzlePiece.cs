﻿using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Puzzle
{
    public class PuzzlePiece : MonoBehaviour
    {
        public string id;

        Vector3 defaultLoc;

        public void UpdateData(string id, string newPiece = "", Action onactionDone = null)
        {
            this.id = id;
            gameObject.SetActive(DataManager.Instance.HasKey(id));

            if (newPiece == id)
            {
                defaultLoc = transform.localPosition;
                transform.localPosition = defaultLoc + Vector3.up * 10;
                transform.localScale = Vector3.one * 1.2f;

                transform.DOLocalMove(defaultLoc, 1f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        transform.DOScale(Vector3.one, 0.5f)
                        .OnComplete(() =>
                        {
                            onactionDone?.Invoke();
                        });
                    });

                transform.DORotate(Vector3.forward * 720f, 1f, RotateMode.FastBeyond360);
            }
        }
    }
}