using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Assets.Scripts.Commons
{
    public class CommonHealthBar : MonoBehaviour
    {
        public Image healthBar;
        public Image healthWhiteBar;

        public float timeAni;
        public void LoadHealthBar(float fillAmount)
        {
            healthBar.fillAmount = fillAmount;

            if (fillAmount != 1)
                DOTween.To(() => healthWhiteBar.fillAmount, x => healthWhiteBar.fillAmount = x, fillAmount, timeAni);
            else
                healthWhiteBar.fillAmount = 1;
        }
    }
}