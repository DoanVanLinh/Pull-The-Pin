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
            settingBtn.onClick.AddListener(delegate { SettingButton(); });
            noAdsBtn.onClick.AddListener(delegate { NoAdsButton(); });
            rateBtn.onClick.AddListener(delegate { RateButton(); });
            playBtn.onClick.AddListener(delegate { PlayButton(); });
        }

        private void PlayButton()
        {
            Close();
        }

        private void RateButton()
        {
            UIManager.Instance.ratePanel.Open();
        }
        public override void Open()
        {
            base.Open();
            transform.DOMove(UIManager.Instance.center, 0.5f);
        }
        public override void Close()
        {
            transform.DOMove(UIManager.Instance.right, 0.5f);
        }
        private void NoAdsButton()
        {
        }

        private void SettingButton()
        {
            UIManager.Instance.settingPanel.Open();
        }

        public override void SaveData()
        {

        }
    }
}