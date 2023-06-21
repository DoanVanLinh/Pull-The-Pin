using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

namespace Assets.Scripts.UI.Win
{
    public class Gift : MonoBehaviour
    {
        public Image giftImg;
        public Image processImg;
        public float timeAni;

        public TextMeshProUGUI percentTxt;

        public void Init()
        {
            int currentPercent = DataManager.Instance.GiftPercent;
            int randomPercent = Random.Range(15, 25);

            randomPercent = 100 < currentPercent + randomPercent ? 100 - currentPercent : randomPercent;

            //Init
            giftImg.fillAmount = currentPercent / 100f;
            processImg.fillAmount = currentPercent / 100f;
            percentTxt.text = currentPercent + "%";

            //data
            DataManager.Instance.AddGiftPercent(randomPercent);

            //ani

            DOTween.To(() => currentPercent, x => currentPercent = x, currentPercent + randomPercent, timeAni)
                    .SetEase(Ease.Linear)
                    .OnUpdate(() =>
                    {
                        giftImg.fillAmount = currentPercent / 100f;
                        processImg.fillAmount = currentPercent / 100f;
                        percentTxt.text = currentPercent + "%";
                    })
                    .OnComplete(() =>
                    {
                        if (currentPercent == 100)
                            StartCoroutine(IEDelay(1f, () => UIManager.Instance.giftPanel.Open()));

                    })
                    .SetUpdate(true);

        }
        IEnumerator IEDelay(float time, Action onDone)
        {
            yield return new WaitForSeconds(time);
            onDone?.Invoke();

        }
    }
}