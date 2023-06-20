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
        private Button skipBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button continuesBtn;

        [FoldoutGroup("Text"), SerializeField]
        private TextMeshProUGUI tipsTxt;
        [FoldoutGroup("Text"), SerializeField]
        private TextMeshProUGUI stageTxt;

        public ELoseType loseType;
        public override void LoadData()
        {
            resumeBtn.onClick.AddListener(delegate { ResumeButton(); });
            skipBtn.onClick.AddListener(delegate { SkipButton(); });
            continuesBtn.onClick.AddListener(delegate { ContinuesButton(); });
            stageTxt.text = (DataManager.Instance.CurrentLevel) + "/" + GameManager.Instance.currentStage.maxLevel + " completed";
            LoadTips();
        }
        private void LoadTips()
        {
            string tips = "";
            switch (loseType)
            {
                case ELoseType.BomBall:
                    tips = "You lose the balls!" +
                        "Get rid of bombs before they destroy it";
                    break;
                case ELoseType.BomBuck:
                    tips = "You lose the bucket!" +
                        "Get rid of bombs before they destroy it";
                    break;
                case ELoseType.LoseBall:
                    tips = "Ball fell out of the level!" +
                        "Avoid gaps to protect your ball from falling";
                    break;
                case ELoseType.CollectGreyBall:
                    tips = "Grey ball can't be collected!" +
                        "Use collored ball to spread the paint";
                    break;
                default:
                    break;
            }
            tipsTxt.text = tips;
        }

        private void SkipButton()
        {
            GameManager.Instance.NextLevel();
            Close();
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
            resumeBtn.onClick.RemoveAllListeners();
            skipBtn.onClick.RemoveAllListeners();
            continuesBtn.onClick.RemoveAllListeners();
        }
    }
}