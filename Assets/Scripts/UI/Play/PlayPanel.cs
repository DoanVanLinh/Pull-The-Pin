﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;

namespace Assets.Scripts.UI.Play
{
    public class PlayPanel : BaseUI
    {

        [FoldoutGroup("Button"), SerializeField]
        private Button replayBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button dailyMissionBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button collectionBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button challegentBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button shoptBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button hometBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button noadstBtn;

        public override void LoadData()
        {
            replayBtn.onClick.AddListener(delegate { ReplayButton(); });
            dailyMissionBtn.onClick.AddListener(delegate { DailyMissionButton(); });
            collectionBtn.onClick.AddListener(delegate { CollectionButton(); });
            challegentBtn.onClick.AddListener(delegate { ChallegentButton(); });
            shoptBtn.onClick.AddListener(delegate { ShopButton(); });
            noadstBtn.onClick.AddListener(delegate { NoadsButton(); });

            GameManager.Instance.SetGameState(GameState.Gameplay);
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
            UIManager.Instance.collectionPanel.Open();
        }

        private void DailyMissionButton()
        {
            UIManager.Instance.dailyMissionPanel.Open();
        }

        private void ReplayButton()
        {
            GameManager.Instance.Replay();
        }

        public override void SaveData()
        {
            replayBtn.onClick.RemoveAllListeners();
            dailyMissionBtn.onClick.RemoveAllListeners();
            collectionBtn.onClick.RemoveAllListeners();
            challegentBtn.onClick.RemoveAllListeners();
            shoptBtn.onClick.RemoveAllListeners();
            noadstBtn.onClick.RemoveAllListeners();
        }
    }
}