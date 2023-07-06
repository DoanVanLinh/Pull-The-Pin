using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.UI.ChallengePlayPanel
{

    public class ChallengePlayPanel : BaseUI
    {
        [FoldoutGroup("Button"), SerializeField]
        private Button closeBtn;
        [FoldoutGroup("Button"), SerializeField]
        private Button homeBtn;
        [FoldoutGroup("Text"), SerializeField]
        private TextMeshProUGUI moveTxt;
        [FoldoutGroup("Text"), SerializeField]
        private TextMeshProUGUI ChallengeTxt;
        public override void LoadData()
        {
            UIManager.Instance.challegentPanel.Close();

            closeBtn.onClick.AddListener(() => CloseButton());
            homeBtn.onClick.AddListener(() => HomeButton());
            UpdateChallengeText();
        }
        public void UpdateMove(int move)
        {
            moveTxt.text = "Move: " + move;
        }
        private void UpdateChallengeText()
        {
            ChallengeTxt.text = "Challenge: " + GameManager.Instance.currentChallenge.id;

        }
        private void HomeButton()
        {
            SoundManager.Instance.Play("Button Click");

            UIManager.Instance.homePanel.Open();
        }

        private void CloseButton()
        {
            SoundManager.Instance.Play("Button Click");

            GameManager.Instance.SetGameState(GameState.NormalMode);
        }

        public override void SaveData()
        {
            UIManager.Instance.challegentPanel.Open();

            closeBtn.onClick.RemoveAllListeners();
            homeBtn.onClick.RemoveAllListeners();
        }
    }
}