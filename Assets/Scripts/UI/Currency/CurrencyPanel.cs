using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.UI.Currency
{
    public class CurrencyPanel : BaseUI
    {
        public TextMeshProUGUI coinsTxt;
        public override void LoadData()
        {
            UpdateCoins();
        }

        public void UpdateCoins()
        {
            coinsTxt.text = DataManager.Instance.Coins.ToString();
        }
        public override void SaveData()
        {
            
        }
    }
}