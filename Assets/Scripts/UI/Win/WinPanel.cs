using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;

namespace Assets.Scripts.UI.Win
{
    public class WinPanel : BasePopupUI
    {
        [FoldoutGroup("Button"), SerializeField]
        private Button continuesBtn;

        public override void LoadData()
        {
            continuesBtn.onClick.AddListener(delegate { ContinuesButton(); });
        }

        private void ContinuesButton()
        {
            GameManager.Instance.NextStage();
            Close();

        }

        public override void SaveData()
        {
            continuesBtn.onClick.RemoveAllListeners();
        }
    }
}