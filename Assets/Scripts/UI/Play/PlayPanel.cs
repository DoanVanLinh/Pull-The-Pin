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

        [FoldoutGroup("Image"), SerializeField]
        private Image bgImg;

        [FoldoutGroup("Noti")]
        public GameObject dailyMissionNoti;
        [FoldoutGroup("Noti")]
        public GameObject shopNoti;
        [FoldoutGroup("Noti")]
        public GameObject challengeNoti;

        [FoldoutGroup("Text"), SerializeField]
        private TextMeshProUGUI stageTxt;
        public override void LoadData()
        {
            
            replayBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); ReplayButton(); });
            dailyMissionBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); DailyMissionButton(); });
            puzzleBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); CollectionButton(); });
            challegentBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); ChallegentButton(); });
            shoptBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); ShopButton(); });
            hometBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); HomeButton(); });
            noadstBtn.onClick.AddListener(delegate { SoundManager.Instance.Play("Button Click"); NoadsButton(); });
            UpdateStageText();
            UpdateShopNoti();
            UpdateChallengeNoti();

            GameManager.Instance.SetGameState(GameState.NormalMode);
        }
        public void UpdateShopNoti()
        {
            bool canGatcha = DataManager.Instance.Coins >= DataManager.Instance.CurrentGatcha * 250 + 500;
            bool newItem = DataManager.Instance.GetData().HasNewItemInShop();
            shopNoti.SetActive(canGatcha||newItem);
        }
        public void UpdateChallengeNoti()
        {
            challengeNoti.SetActive(DataManager.Instance.GetData().HasNewChallenge());
        }
        private void HomeButton()
        {
            Time.timeScale = 0;
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
        public void SetBG(Sprite bg)
        {
            bgImg.sprite = bg;
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