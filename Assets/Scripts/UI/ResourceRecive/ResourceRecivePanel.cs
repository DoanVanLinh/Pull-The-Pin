using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.UI.ResourceRecive
{
    public class ResourceRecivePanel : BaseUI
    {
        [FoldoutGroup("Coins Recive"), SerializeField]
        private List<Image> iconCoins;
        [FoldoutGroup("Coins Recive"), SerializeField]
        private Transform coinsPanelTransform;

        public override void LoadData()
        {
            int length = iconCoins.Count;

            for (int i = 0; i < length; i++)
            {
                iconCoins[i].gameObject.SetActive(false);
            }


        }
        private Image GetImagePooling(List<Image> lists)
        {
            int length = lists.Count;
            for (int i = 0; i < length; i++)
            {
                if (!lists[i].gameObject.activeSelf)
                    return lists[i];
            }

            return null;
        }
        private void AnimationTransform(Transform obj, Vector3 initPosition, Vector3 position, Vector3 targetPosition, Action onAniDone)
        {
            obj.gameObject.SetActive(true);
            obj.position = initPosition;
            obj.localScale = Vector3.one;


            obj.DOMove(position, 0.5f)
                    .SetEase(Ease.Linear)
                    .SetUpdate(true);

            obj.DOScale(Vector3.one * 1.5f, 0.5f)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    obj.DOMove(targetPosition, 0.5f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        obj.gameObject.SetActive(false);
                        onAniDone?.Invoke();
                    }).SetUpdate(true);
                }).SetUpdate(true);

        }
        public void CoinsRecive(Vector3 position, Action onActionDone, int amount = 10)
        {
            for (int i = 0; i < amount; i++)
            {
                Image img = GetImagePooling(iconCoins);
                if (img != null)
                    AnimationTransform(img.transform, position, i == 0 ? position : (Vector2)position + (Random.insideUnitCircle * 3f), coinsPanelTransform.position, i == 0 ? onActionDone : null);
            }
        }


        public override void SaveData()
        {
        }
    }
}