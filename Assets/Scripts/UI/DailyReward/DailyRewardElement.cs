using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Assets.Scripts.UI.ResourceRecive;

namespace Assets.Scripts.UI.DailyReward
{
    public class DailyRewardElement : MonoBehaviour
    {
        public int id = 0;
        public EDailyRewardType type;
        public int amount;

        public Button claimBtn;
        public GameObject claimedImg;
        public GameObject coinsImg;
        public GameObject notifi;

        public TextMeshProUGUI amountTxt;
        void OnEnable()
        {
            amountTxt.gameObject.SetActive(type == EDailyRewardType.Coins);
            coinsImg.gameObject.SetActive(type == EDailyRewardType.Coins);

            claimBtn.onClick.AddListener(delegate { ClaimButton(); });

            claimedImg.SetActive(DataManager.Instance.CountDailyReward % 6 > id);
            claimBtn.interactable = DataManager.Instance.CountDailyReward % 6 == id;
            notifi.SetActive(DataManager.Instance.CountDailyReward % 6 == id);
            amountTxt.text = "+" + amount;

        }

        private void ClaimButton()
        {
            claimBtn.interactable = false;
            switch (type)
            {
                case EDailyRewardType.Coins:
                    DataManager.Instance.AddCoins(amount);
                    ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(transform.position,
                                   delegate
                                   {
                                       UIManager.Instance.dailyRewardPanel.Close();
                                       UIManager.Instance.currentcyPanel.Close();
                                   });
                    break;
                case EDailyRewardType.Item:
                    break;
                case EDailyRewardType.Random:
                    break;
                default:
                    break;
            }

            CPlayerPrefs.SetBool(DateTime.Now.ToString("d"), true);
            DataManager.Instance.AddCountDailyReward();

        }

        private void OnDisable()
        {
            claimBtn.onClick.RemoveAllListeners();
        }

    }
}