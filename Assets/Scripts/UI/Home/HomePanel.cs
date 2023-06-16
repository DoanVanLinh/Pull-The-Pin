using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;

namespace Assets.Scripts.UI.Home
{
    public class HomePanel : BaseUI
    {
        [FoldoutGroup("Ani"), SerializeField]
        private Animator ani;

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
            ani.Play("Close");
        }

        private void RateButton()
        {
            UIManager.Instance.ratePanel.Open();

        }

        private void NoAdsButton()
        {
            //ads
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