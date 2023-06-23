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
        public Animator ani;

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

        public override void Open()
        {
            base.Open();
            ani.Play("Open");
        }
        public override void LoadData()
        {
            Time.timeScale = 0;
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
                    tips = "You lose the balls!\n" +
                        "Get rid of bombs before they destroy it";
                    break;
                case ELoseType.BomBuck:
                    tips = "You lose the bucket!\n" +
                        "Get rid of bombs before they destroy it";
                    break;
                case ELoseType.LoseBall:
                    tips = "Ball fell out of the level!\n" +
                        "Avoid gaps to protect your ball from falling";
                    break;
                case ELoseType.CollectGreyBall:
                    tips = "Grey ball can't be collected!\n" +
                        "Use collored ball to spread the paint";
                    break;
                default:
                    break;
            }
            tipsTxt.text = tips;
        }

        private void SkipButton()
        {
            GameManager.Instance.ShowAdsReward(Helper.Skip_Level_Placement, () =>
            {
                GameManager.Instance.NextLevel();
                ani.Play("Close");
            });

        }

        private void ContinuesButton()
        {
            GameManager.Instance.ReplayStage();
            ani.Play("Close");
        }

        private void ResumeButton()
        {
            GameManager.Instance.ShowAdsReward(Helper.Resume_Level_Placement, () =>
            {
                GameManager.Instance.ReplayLevel();
                ani.Play("Close");
            });
            
        }

        public override void SaveData()
        {
            Time.timeScale = 1;

            resumeBtn.onClick.RemoveAllListeners();
            skipBtn.onClick.RemoveAllListeners();
            continuesBtn.onClick.RemoveAllListeners();
        }
    }
}