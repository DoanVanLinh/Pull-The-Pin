using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;

namespace Assets.Scripts.UI.Play
{
    public class PlayPanel : BaseUI
    {
        [FoldoutGroup("Component")]
        public StagePanel stagePanel;

        [FoldoutGroup("Button"), SerializeField]
        private Button replayBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button dailyMissionBtn;
        [FoldoutGroup("Button")]
        public Button puzzleBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button challegentBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button shoptBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button hometBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button noadstBtn;

        [FoldoutGroup("Noti")]
        public GameObject dailyMissionNoti;

        [FoldoutGroup("Text"), SerializeField]
        private TextMeshProUGUI stageTxt;
        public override void LoadData()
        {
            replayBtn.onClick.AddListener(delegate { ReplayButton(); });
            dailyMissionBtn.onClick.AddListener(delegate { DailyMissionButton(); });
            puzzleBtn.onClick.AddListener(delegate { CollectionButton(); });
            challegentBtn.onClick.AddListener(delegate { ChallegentButton(); });
            shoptBtn.onClick.AddListener(delegate { ShopButton(); });
            hometBtn.onClick.AddListener(delegate { HomeButton(); });
            noadstBtn.onClick.AddListener(delegate { NoadsButton(); });
            UpdateStageText();

            GameManager.Instance.SetGameState(GameState.Gameplay);
        }

        private void HomeButton()
        {
            UIManager.Instance.homePanel.Open();
        }

        public void UpdateStageText()
        {
            stageTxt.text = (DataManager.Instance.CurrentStage+1).ToString();

        }
        private void NoadsButton()
        {
            //ads
            //
        }

        private void ShopButton()
        {
            UIManager.Instance.shopPanel.Open();
        }

        private void ChallegentButton()
        {
            UIManager.Instance.challegentPanel.Open();
        }

        private void CollectionButton()
        {
            UIManager.Instance.puzzleGroupPanel.Open();
        }

        private void DailyMissionButton()
        {
            UIManager.Instance.dailyMissionPanel.Open();
        }

        private void ReplayButton()
        {
            GameManager.Instance.ReplayLevel();
        }

        public override void SaveData()
        {
            replayBtn.onClick.RemoveAllListeners();
            dailyMissionBtn.onClick.RemoveAllListeners();
            puzzleBtn.onClick.RemoveAllListeners();
            challegentBtn.onClick.RemoveAllListeners();
            shoptBtn.onClick.RemoveAllListeners();
            hometBtn.onClick.RemoveAllListeners();
            noadstBtn.onClick.RemoveAllListeners();
        }
    }
}