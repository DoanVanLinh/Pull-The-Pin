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
        public ERewardType type;
        public int amount;

        public Button claimBtn;
        public GameObject claimedImg;
        public GameObject coinsImg;
        public GameObject notifi;

        public TextMeshProUGUI amountTxt;
        void OnEnable()
        {
            amountTxt.gameObject.SetActive(type == ERewardType.Coins);
            coinsImg.gameObject.SetActive(type == ERewardType.Coins);

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
                case ERewardType.Coins:
                    DataManager.Instance.AddCoins(amount);
                    ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(transform.position,
                                   delegate
                                   {
                                       ((DailyRewardPanel)UIManager.Instance.dailyRewardPanel).CloseButton();
                                       UIManager.Instance.currentcyPanel.Close();
                                   });
                    break;
                case ERewardType.Item:
                    break;
                case ERewardType.Random:
                    break;
                default:
                    break;
            }
            claimedImg.SetActive(true);
            notifi.SetActive(false);
            CPlayerPrefs.SetBool(DateTime.Now.ToString("d"), true);
            DataManager.Instance.AddCountDailyReward();

        }

        private void OnDisable()
        {
            claimBtn.onClick.RemoveAllListeners();
        }

    }
}