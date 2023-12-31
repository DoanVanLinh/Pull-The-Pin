﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using Assets.Scripts.UI.ResourceRecive;
using Assets.Scripts.UI.Play;

namespace Assets.Scripts.UI.Win
{
    public class WinPanel : BasePopupUI
    {
        public Animator ani;
        [FoldoutGroup("Button"), SerializeField]
        private Button continuesBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button extraCoinsBtn;

        public TextMeshProUGUI coins;
        public TextMeshProUGUI coinsButton;
        public Gift gift;
        public Extra extra;
        private int amountCoins;
        public override void LoadData()
        {
            SetInterrect(true);

            amountCoins = Random.Range(80, 110);
            coins.text = amountCoins.ToString();
            coinsButton.text = amountCoins.ToString();
            continuesBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); ContinuesButton(); });
            extraCoinsBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); ExtraCoinsButton(); });
            gift.Init();
            extra.Init(amountCoins);
            ani.Play("Open");
            UIManager.Instance.currentcyPanel.Open();

        }

        private void ExtraCoinsButton()
        {
            GameManager.Instance.ShowAdsReward(Helper.Extra_Coins_Win_Placement, () =>
            {
                SetInterrect(false);

                extra.running = false;
                GameManager.Instance.NextStage();
                ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(extraCoinsBtn.transform.position, () =>
                {
                    SoundManager.Instance.Play("GetCoins");
                    DataManager.Instance.AddCoins(amountCoins * ExtraElement.currentElement.extra);

                    ani.Play("Close");

                });
                ((PlayPanel)UIManager.Instance.gamePlayPanel).UpdateStageText();
            });

        }
        private void SetInterrect(bool interract)
        {
            continuesBtn.interactable = interract;
            extraCoinsBtn.interactable = interract;
        }
        private void ContinuesButton()
        {
            SetInterrect(false);
            GameManager.Instance.NextStage();
            ((ResourceRecivePanel)UIManager.Instance.resorceRecivePanel).CoinsRecive(continuesBtn.transform.position, () =>
            {
                SoundManager.Instance.Play("GetCoins");
                DataManager.Instance.AddCoins(amountCoins);

                ani.Play("Close");
            });
            ((PlayPanel)UIManager.Instance.gamePlayPanel).UpdateStageText();
        }

        public override void SaveData()
        {
            base.Close();

            UIManager.Instance.currentcyPanel.Close();

            continuesBtn.onClick.RemoveAllListeners();
            extraCoinsBtn.onClick.RemoveAllListeners();
        }
    }
}