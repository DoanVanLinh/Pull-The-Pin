using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;

namespace Assets.Scripts.UI.Win
{
    public class WinPopup : BaseUI
    {
        [FoldoutGroup("Button"), SerializeField]
        private Button continuesBtn;

        public override void LoadData()
        {
            continuesBtn.onClick.AddListener(delegate { ContinuesButton(); });
        }

        private void ContinuesButton()
        {
            DataManager.Instance.AddCurrentState(1);
            DataManager.Instance.SetCurrentLevel(1);


        }

        public override void SaveData()
        {
            continuesBtn.onClick.RemoveAllListeners();
        }
    }
}