using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;

namespace Assets.Scripts.UI.Lose
{
    public class LosePanel : BaseUI
    {
        [FoldoutGroup("Button"), SerializeField]
        private Button resumeBtn;

        [FoldoutGroup("Button"), SerializeField]
        private Button continuesBtn;

        public override void LoadData()
        {
            resumeBtn.onClick.AddListener(delegate { ResumeButton(); });
            continuesBtn.onClick.AddListener(delegate { ContinuesButton(); });

        }

        private void ContinuesButton()
        {
            GameManager.Instance.ReplayStage();
            Close();
        }

        private void ResumeButton()
        {
            GameManager.Instance.ReplayLevel();
            Close();
        }

        public override void SaveData()
        {
        }
    }
}