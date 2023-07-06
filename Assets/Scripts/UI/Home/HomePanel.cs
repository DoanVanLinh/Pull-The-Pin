using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;
using DG.Tweening;

namespace Assets.Scripts.UI.Home
{
    public class HomePanel : BaseUI
    {
        [FoldoutGroup("Button"), SerializeField]
        private Button settingBtn;

        [FoldoutGroup("Button"), SerializeField]
        private Button noAdsBtn;

        [FoldoutGroup("Button"), SerializeField]
        private Button rateBtn;

        [FoldoutGroup("Button"), SerializeField]
        private Button playBtn;

        public override void LoadData()
        {
            Time.timeScale = 0;
            settingBtn.onClick.AddListener(delegate { SettingButton(); });
            noAdsBtn.onClick.AddListener(delegate { NoAdsButton(); });
            rateBtn.onClick.AddListener(delegate { RateButton(); });
            playBtn.onClick.AddListener(delegate { PlayButton(); });

            CheckDailyReward();
        }
        private void CheckDailyReward()
        {
            if (CPlayerPrefs.GetBool(DateTime.Now.ToString("d"), false))
                return;

            UIManager.Instance.dailyRewardPanel.Open();
        }

        private void PlayButton()
        {
            SoundManager.Instance.Play("Button Click");

            Close();
            Time.timeScale = 1;
        }

        private void RateButton()
        {
            SoundManager.Instance.Play("Button Click");
            UIManager.Instance.ratePanel.Open();
        }
        public override void Open()
        {
            base.Open();
            transform.DOMove(UIManager.Instance.center, 0.5f).SetUpdate(true);
        }
        public override void Close()
        {
            transform.DOMove(UIManager.Instance.right, 0.5f).SetUpdate(true);
        }
        private void NoAdsButton()
        {
            SoundManager.Instance.Play("Button Click");

        }

        private void SettingButton()
        {
            SoundManager.Instance.Play("Button Click");
            UIManager.Instance.settingPanel.Open();
            transform.DOMove(UIManager.Instance.left, 0.25f).SetUpdate(true);
        }

        public override void SaveData()
        {

        }
    }
}