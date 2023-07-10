using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Assets.Scripts.UI.Currency
{
    public class CurrencyPanel : BaseUI
    {
        public TextMeshProUGUI coinsTxt;
        public Animator ani;
        public override void LoadData()
        {
            UpdateCoins();
        }

        public void UpdateCoins()
        {
            coinsTxt.text = DataManager.Instance.Coins.ToString();
            ani.Play("Collect");
        }
        public override void SaveData()
        {
            
        }
    }
}