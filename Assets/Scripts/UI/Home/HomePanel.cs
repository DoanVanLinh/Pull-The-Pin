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
        [FoldoutGroup("Button"), SerializeField]
        private Button settingBtn;

        [FoldoutGroup("Button"), SerializeField]
        private Button noAdsBtn;

        [FoldoutGroup("Button"), SerializeField]
        private Button rateBtn;

        public override void LoadData()
        {
            settingBtn.onClick.AddListener(delegate { SettingButton(); });
            noAdsBtn.onClick.AddListener(delegate { NoAdsButton(); });
            rateBtn.onClick.AddListener(delegate { RateButton(); });
        }

        private void RateButton()
        {
            throw new NotImplementedException();
        }

        private void NoAdsButton()
        {
            throw new NotImplementedException();
        }

        private void SettingButton()
        {
            throw new NotImplementedException();
        }

        public override void SaveData()
        {
        }
    }
}